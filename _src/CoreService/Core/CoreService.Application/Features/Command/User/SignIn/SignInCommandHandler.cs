using CoreService.Application.Models;
using CoreService.Application.Repositories;
using CoreService.Domain.AggregateRoots.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading;

namespace CoreService.Application.Features.Command.User.SignIn
{
    internal class SignInCommandHandler(ILogger<SignInCommandHandler> logger, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, AppSettings appSettings) : BaseCqrsAndDomainEventHandler<SignInCommandHandler>(logger), IRequestHandler<SignInCommandRequest, SignInCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly AppSettings _appSettings = appSettings;

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
                var checkUser = await _unitOfWork.UserReadRepository.FindByConditionAsNoTracking(u => u.Email == request.EmailOrUsername || u.Username == request.EmailOrUsername).Select(u => new
                {
                    u.PasswordHash,
                    u.PasswordSalt
                }).FirstOrDefaultAsync(cancellationToken);
                if (checkUser == null)
                {
                    LogWarning("Your email or username is wrong", request, HttpStatusCode.BadRequest);
                    response.SetForError("Your email or username is wrong", HttpStatusCode.BadRequest);
                    return response;
                }
                else if (checkUser.PasswordHash != UserEntity.ComputeHash(request.Password, checkUser.PasswordSalt))
                {
                    LogWarning("Your password is wrong", request, HttpStatusCode.BadRequest);
                    response.SetForError("Your password is wrong", HttpStatusCode.BadRequest);
                    return response;
                }
                var foundUser = await _unitOfWork.UserReadRepository.FindByConditionAsNoTracking(u => u.Email == request.EmailOrUsername || u.Username == request.EmailOrUsername).Include(u => u.Profile).ThenInclude(p => p.Company).Include(u => u.Role).ThenInclude(r => r.Screens).FirstOrDefaultAsync(cancellationToken);
                if (foundUser == null)
                {
                    LogWarning("Something went wrong while retrieving your user information", request, HttpStatusCode.BadRequest);
                    response.SetForError("Something went wrong while retrieving your user information", HttpStatusCode.BadRequest);
                    return response;
                }
                SetCookiesToResponse(
                    foundUser.GenerateToken(TimeSpan.FromMinutes(_appSettings.GenerateTokenSettings.AccessTokenExpireTime), _appSettings.GenerateTokenSettings.SecretKey, _appSettings.GenerateTokenSettings.Issuer, _appSettings.GenerateTokenSettings.Audience),
                    foundUser.GenerateToken(TimeSpan.FromMinutes(_appSettings.GenerateTokenSettings.RefreshTokenExpireTime), _appSettings.GenerateTokenSettings.SecretKey, _appSettings.GenerateTokenSettings.Issuer, _appSettings.GenerateTokenSettings.Audience)
                    );
                var allScreenNamesOfUser = foundUser.Role?.Screens?.Select(s => s.Name)?.ToHashSet() ?? new HashSet<string>();
                response.SetPayload(
                    SignInCommandResponseModel.CreateNewSignInCommandResponseModel(foundUser.Id, foundUser.Username, foundUser.Email, foundUser.Profile?.Firstname, foundUser.Profile?.Lastname, foundUser.Profile?.Company?.Name, allScreenNamesOfUser)
                    );
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
            SetCookiesToResponse(
                    adminUser.GenerateToken(TimeSpan.FromMinutes(_appSettings.GenerateTokenSettings.AccessTokenExpireTime), _appSettings.GenerateTokenSettings.SecretKey, _appSettings.GenerateTokenSettings.Issuer, _appSettings.GenerateTokenSettings.Audience),
                    adminUser.GenerateToken(TimeSpan.FromMinutes(_appSettings.GenerateTokenSettings.RefreshTokenExpireTime), _appSettings.GenerateTokenSettings.SecretKey, _appSettings.GenerateTokenSettings.Issuer, _appSettings.GenerateTokenSettings.Audience)
                    );
            response.SetPayload(
            SignInCommandResponseModel.CreateNewSignInCommandResponseModel(adminUser.Id, adminUser.Username, adminUser.Email, adminUser.Profile?.Firstname, adminUser.Profile?.Lastname, adminUser.Profile?.Company?.Name, allScreenNamesOfAdminUser)
            );
        }
    }
}