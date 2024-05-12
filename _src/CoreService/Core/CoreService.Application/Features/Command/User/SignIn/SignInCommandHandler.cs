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
                var foundUser = await _unitOfWork.UserReadRepository.FindByConditionAsNoTracking(u => u.Email == request.EmailOrUsername || u.Username == request.EmailOrUsername).Include(u => u.Profile).ThenInclude(p => p.Company).Include(u => u.Role).ThenInclude(r => r.Screens).FirstOrDefaultAsync(cancellationToken);
                if (foundUser == null)
                {
                    LogWarning("Your email or username is wrong", request, HttpStatusCode.BadRequest);
                    response.SetForError("Your email or username is wrong", HttpStatusCode.BadRequest);
                    return response;
                }
                else if (foundUser.PasswordHash != UserEntity.ComputeHash(request.Password, foundUser.PasswordSalt))
                {
                    LogWarning("Your password is wrong", request, HttpStatusCode.BadRequest);
                    response.SetForError("Your password is wrong", HttpStatusCode.BadRequest);
                    return response;
                }

                var generatedAccessToken = foundUser.GenerateToken(TimeSpan.FromMinutes(_appSettings.GenerateTokenSettings.AccessTokenExpireTime), _appSettings.GenerateTokenSettings.SecretKey, _appSettings.GenerateTokenSettings.Issuer, _appSettings.GenerateTokenSettings.Audience);
                var generatedRefreshToken = foundUser.GenerateToken(TimeSpan.FromMinutes(_appSettings.GenerateTokenSettings.RefreshTokenExpireTime), _appSettings.GenerateTokenSettings.SecretKey, _appSettings.GenerateTokenSettings.Issuer, _appSettings.GenerateTokenSettings.Audience);
                SetCookiesToResponse(generatedAccessToken, generatedRefreshToken);

                var allScreenNamesOfUser = foundUser.Role?.Screens?.Select(s => s.Name)?.ToHashSet() ?? new HashSet<string>();
                response.SetPayload(
                    SignInCommandResponseModel.CreateNewSignInCommandResponseModel(foundUser.Id, foundUser.Username, foundUser.Email, foundUser.Profile?.Firstname, foundUser.Profile?.Lastname, foundUser.Profile?.Company?.Name, foundUser.Role?.Name ?? string.Empty, allScreenNamesOfUser)
                    );

                await SendTokenWithRabbitMqMessage(generatedAccessToken, generatedRefreshToken, foundUser.Id.ToString(), cancellationToken);
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
            var adminUser = await _unitOfWork.UserReadRepository.FindByConditionAsNoTracking(u => u.Username == request.EmailOrUsername).Include(u => u.Profile).ThenInclude(p => p.Company).Include(u => u.Role).FirstOrDefaultAsync(cancellationToken);
            if (adminUser is null)
            {
                LogWarning("Your admin credentials are wrong", request, HttpStatusCode.BadRequest);
                response.SetForError("Your admin credentials are wrong", HttpStatusCode.BadRequest);
                return;
            }
            var allScreenNamesOfAdminUser = await _unitOfWork.ScreenReadRepository.GetOnlyScreenNamesAsNoTrackingAsync(cancellationToken);

            var generatedAccessToken = adminUser.GenerateToken(TimeSpan.FromMinutes(_appSettings.GenerateTokenSettings.AccessTokenExpireTime), _appSettings.GenerateTokenSettings.SecretKey, _appSettings.GenerateTokenSettings.Issuer, _appSettings.GenerateTokenSettings.Audience);
            var generatedRefreshToken = adminUser.GenerateToken(TimeSpan.FromMinutes(_appSettings.GenerateTokenSettings.RefreshTokenExpireTime), _appSettings.GenerateTokenSettings.SecretKey, _appSettings.GenerateTokenSettings.Issuer, _appSettings.GenerateTokenSettings.Audience);

            SetCookiesToResponse(generatedAccessToken, generatedRefreshToken);

            response.SetPayload(
            SignInCommandResponseModel.CreateNewSignInCommandResponseModel(adminUser.Id, adminUser.Username, adminUser.Email, adminUser.Profile?.Firstname, adminUser.Profile?.Lastname, adminUser.Profile?.Company?.Name, adminUser.Role?.Name ?? string.Empty, allScreenNamesOfAdminUser)
            );
            await SendTokenWithRabbitMqMessage(generatedAccessToken, generatedRefreshToken, adminUser.Id.ToString(), cancellationToken);
        }

        private async Task SendTokenWithRabbitMqMessage(string generatedAccessToken, string generatedRefreshToken, string userId, CancellationToken cancellationToken)
        {
            await _eventBusProvider._eventBus.Send<SendUserTokenMessageCommand>(new SendUserTokenMessageCommand()
            {
                AccessToken = generatedAccessToken,
                RefreshToken = generatedRefreshToken,
                UserId = userId,
                AccessTokenExpireDate = (UInt64)DateTimeOffset.UtcNow.AddMinutes(_appSettings.GenerateTokenSettings.AccessTokenExpireTime).ToUnixTimeSeconds(),
                RefreshTokenExpireDate = (UInt64)DateTimeOffset.UtcNow.AddMinutes(_appSettings.GenerateTokenSettings.RefreshTokenExpireTime).ToUnixTimeSeconds(),
            }, cancellationToken);
        }
    }
}