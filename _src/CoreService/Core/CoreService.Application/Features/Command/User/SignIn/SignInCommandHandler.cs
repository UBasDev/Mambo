using CoreService.Application.DTOs;
using CoreService.Application.Models;
using CoreService.Application.Repositories;
using CoreService.Domain.AggregateRoots.User;
using Mambo.MassTransit.Concretes;
using Mambo.MassTransit.Contracts.Events.Commands.Concretes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CoreService.Application.Features.Command.User.SignIn
{
    internal class SignInCommandHandler(ILogger<SignInCommandHandler> logger, IUnitOfWork _unitOfWork, IHttpContextAccessor _httpContextAccessor, AppSettings _appSettings, PublisherEventBusProvider _eventBusProvider) : BaseCqrsAndDomainEventHandler<SignInCommandHandler>(logger), IRequestHandler<SignInCommandRequest, SignInCommandResponse>
    {
        public async Task<SignInCommandResponse> Handle(SignInCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new SignInCommandResponse();
            try
            {
                if (request.EmailOrUsername == _appSettings.UiAdminUsername && request.Password == _appSettings.UiAdminPassword)
                {
                    await HandleForAdminUserAsync(request, response, cancellationToken);
                    return response;
                }
                var foundUser = await _unitOfWork.UserReadRepository.GetUserWithAllIncludesAsNoTrackingAsync(request.EmailOrUsername, cancellationToken);
                if (foundUser == null)
                {
                    LogWarning("Your email or username is wrong", request, HttpStatusCode.BadRequest);
                    response.SetForError("Your email or username is wrong", HttpStatusCode.BadRequest);
                    return response;
                }
                else if (foundUser.PasswordHash != UserEntity.ComputeHash(request.Password, foundUser.PasswordSalt ?? string.Empty))
                {
                    LogWarning("Your password is wrong", request, HttpStatusCode.BadRequest);
                    response.SetForError("Your password is wrong", HttpStatusCode.BadRequest);
                    return response;
                }

                var generatedAccessToken = foundUser.GenerateToken(TimeSpan.FromMinutes(_appSettings.GenerateTokenSettings.AccessTokenExpireTime), _appSettings.GenerateTokenSettings.SecretKey, _appSettings.GenerateTokenSettings.Issuer, _appSettings.GenerateTokenSettings.Audience);
                var generatedRefreshToken = foundUser.GenerateToken(TimeSpan.FromMinutes(_appSettings.GenerateTokenSettings.RefreshTokenExpireTime), _appSettings.GenerateTokenSettings.SecretKey, _appSettings.GenerateTokenSettings.Issuer, _appSettings.GenerateTokenSettings.Audience);
                SetCookiesToResponse(generatedAccessToken, generatedRefreshToken);

                response.SetPayload(
                    SignInCommandResponseModel.CreateNewSignInCommandResponseModel(foundUser.Username, foundUser.Email, foundUser.Firstname, foundUser.Lastname, foundUser.CompanyName, foundUser.RoleName, foundUser.RoleLevel, foundUser.Screens)
                    );

                await SendTokenWithRabbitMqMessage(generatedAccessToken, generatedRefreshToken, foundUser.Id, foundUser.Email, cancellationToken);
            }
            catch (Exception ex)
            {
                LogError("Unable to sign in", ex, request, HttpStatusCode.InternalServerError);
                response.SetForError("Unexpected error happened while signing in", HttpStatusCode.InternalServerError);
            }
            return response;
        }

        private void SetCookiesToResponse(string accessToken, string refreshToken)
        {
            _httpContextAccessor?.HttpContext?.Response.Cookies.Append(
                    key: "mambo-access-token",

                    value: accessToken,

                    new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = false,
                        IsEssential = true,
                        SameSite = SameSiteMode.Strict,
                        Domain = "localhost",
                        MaxAge = TimeSpan.FromMinutes(_appSettings.GenerateTokenSettings.AccessTokenExpireTime),
                        Path = "/"
                    }
                );
            _httpContextAccessor?.HttpContext?.Response.Cookies.Append(
                key: "mambo-refresh-token",

                value: refreshToken,

                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    IsEssential = true,
                    SameSite = SameSiteMode.Strict,
                    Domain = "localhost",
                    MaxAge = TimeSpan.FromMinutes(_appSettings.GenerateTokenSettings.RefreshTokenExpireTime),
                    Path = "/"
                }
            );
        }

        private async Task HandleForAdminUserAsync(SignInCommandRequest request, SignInCommandResponse response, CancellationToken cancellationToken)
        {
            var adminUser = await _unitOfWork.UserReadRepository.GetAdminUserWithAllIncludesAsNoTrackingAsync(request.EmailOrUsername, cancellationToken);

            if (adminUser is null)
            {
                LogWarning("Your admin credentials are wrong", request, HttpStatusCode.BadRequest);
                response.SetForError("Your admin credentials are wrong", HttpStatusCode.BadRequest);
                return;
            }

            var allScreenNamesOfAdminUser = await _unitOfWork.ScreenReadRepository.GetOnlyScreenNamesAsNoTrackingAsync(cancellationToken);

            adminUser.AddScreensRange(allScreenNamesOfAdminUser);

            var generatedAccessToken = adminUser.GenerateToken(TimeSpan.FromMinutes(_appSettings.GenerateTokenSettings.AccessTokenExpireTime), _appSettings.GenerateTokenSettings.SecretKey, _appSettings.GenerateTokenSettings.Issuer, _appSettings.GenerateTokenSettings.Audience);
            var generatedRefreshToken = adminUser.GenerateToken(TimeSpan.FromMinutes(_appSettings.GenerateTokenSettings.RefreshTokenExpireTime), _appSettings.GenerateTokenSettings.SecretKey, _appSettings.GenerateTokenSettings.Issuer, _appSettings.GenerateTokenSettings.Audience);

            SetCookiesToResponse(generatedAccessToken, generatedRefreshToken);

            response.SetPayload(
            SignInCommandResponseModel.CreateNewSignInCommandResponseModel(adminUser.Username, adminUser.Email, adminUser.Firstname, adminUser.Lastname, adminUser.CompanyName, adminUser.RoleName, adminUser.RoleLevel, allScreenNamesOfAdminUser)
            );
            await SendTokenWithRabbitMqMessage(generatedAccessToken, generatedRefreshToken, adminUser.Id, adminUser.Email, cancellationToken);
        }

        private async Task SendTokenWithRabbitMqMessage(string generatedAccessToken, string generatedRefreshToken, string userId, string email, CancellationToken cancellationToken)
        {
            await _eventBusProvider._eventBus.Send<SendUserTokenMessageCommand>(new SendUserTokenMessageCommand()
            {
                AccessToken = generatedAccessToken,
                RefreshToken = generatedRefreshToken,
                UserId = userId,
                AccessTokenExpireDate = (UInt64)DateTimeOffset.UtcNow.AddMinutes(_appSettings.GenerateTokenSettings.AccessTokenExpireTime).ToUnixTimeSeconds(),
                RefreshTokenExpireDate = (UInt64)DateTimeOffset.UtcNow.AddMinutes(_appSettings.GenerateTokenSettings.RefreshTokenExpireTime).ToUnixTimeSeconds(),
                Email = email
            }, cancellationToken);
        }
    }
}