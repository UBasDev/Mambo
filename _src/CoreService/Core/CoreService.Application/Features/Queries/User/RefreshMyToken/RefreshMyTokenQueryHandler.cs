using CoreService.Application.DTOs;
using CoreService.Application.Models;
using CoreService.Application.Repositories;
using Mambo.Helper;
using Mambo.JWT;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Application.Features.Queries.User.RefreshMyToken
{
    internal sealed class RefreshMyTokenQueryHandler(ILogger<RefreshMyTokenQueryHandler> _logger, IHttpContextAccessor _httpContextAccessor, AppSettings _appSettings, IUnitOfWork _unitOfWork, Helpers _helpers) : BaseCqrsAndDomainEventHandler<RefreshMyTokenQueryHandler>(_logger), IRequestHandler<RefreshMyTokenQueryRequest, RefreshMyTokenQueryResponse>
    {
        public async Task<RefreshMyTokenQueryResponse> Handle(RefreshMyTokenQueryRequest request, CancellationToken cancellationToken)
        {
            var response = new RefreshMyTokenQueryResponse();
            try
            {
                if (_httpContextAccessor.HttpContext != null)
                {
                    _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(_appSettings.CookieSettings.RefreshTokenCookieKey, out string? token);
                    if (token is null)
                    {
                        _httpContextAccessor.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                        return response;
                    }
                    var (isTokenValid, errorMessage) = await ValidateRefreshTokenFromCookie(token, cancellationToken);
                    if (!isTokenValid)
                    {
                        LogWarning("This refresh token couldn't be verified", errorMessage, request, HttpStatusCode.Forbidden);
                        response.SetForError("Your session is expired", HttpStatusCode.Forbidden);
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                LogError("Unexpected error occurred while refreshing your token", ex, request, HttpStatusCode.InternalServerError);
                response.SetForError("Unexpected error occurred while refreshing your token", HttpStatusCode.InternalServerError);
            }
            return response;
        }

        private async Task<(bool isRefreshTokenValid, string? errorMessage)> ValidateRefreshTokenFromCookie(string refreshToken, CancellationToken cancellationToken)
        {
            var jwtHandler = new JwtSecurityTokenHandler()
            {
                MapInboundClaims = _appSettings.JwtSettings.MapInboundClaims
            };
            try
            {
                var principal = jwtHandler.ValidateToken(
            refreshToken,
                new TokenValidationParameters()
                {
                    ValidateLifetime = _appSettings.JwtSettings.ValidateLifetime,

                    ValidateAudience = _appSettings.JwtSettings.ValidateAudience,

                    ValidateIssuer = _appSettings.JwtSettings.ValidateIssuer,

                    ValidateIssuerSigningKey = _appSettings.JwtSettings.ValidateIssuerSigningKey,

                    ClockSkew = TimeSpan.Zero,

                    RequireSignedTokens = _appSettings.JwtSettings.RequireSignedTokens,

                    RequireExpirationTime = _appSettings.JwtSettings.RequireExpirationTime,

                    RequireAudience = _appSettings.JwtSettings.ValidateAudience,

                    SaveSigninToken = _appSettings.JwtSettings.SaveToken,

                    NameClaimType = _appSettings.JwtSettings.NameClaimType,

                    RoleClaimType = _appSettings.JwtSettings.RoleClaimType,

                    LogValidationExceptions = _appSettings.JwtSettings.LogValidationExceptions,

                    ValidTypes = _appSettings.JwtSettings.ValidTypes,

                    ValidAlgorithms = _appSettings.JwtSettings.ValidAlgorithms,

                    ValidIssuers = _appSettings.JwtSettings.ValidIssuers,

                    ValidAudiences = _appSettings.JwtSettings.ValidAudiences,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JwtSettings.SecretKey))
                },
                out _
            );
                var username = principal.Claims.FirstOrDefault(c => c.Type == "username")?.Value;

                if (username == null) return (false, "This token doesn't have valid claims");

                var foundUser = await _unitOfWork.UserReadRepository.GetOnlyTokenFieldsAsNoTrackingAsync(username, cancellationToken);

                if (foundUser == null) return (false, "This token doesn't belong any user");

                var generatedAccessToken = foundUser.GenerateToken(TimeSpan.FromMinutes(_appSettings.GenerateTokenSettings.AccessTokenExpireTime), _appSettings.GenerateTokenSettings.SecretKey, _appSettings.GenerateTokenSettings.Issuer, _appSettings.GenerateTokenSettings.Audience);
                var generatedRefreshToken = foundUser.GenerateToken(TimeSpan.FromMinutes(_appSettings.GenerateTokenSettings.RefreshTokenExpireTime), _appSettings.GenerateTokenSettings.SecretKey, _appSettings.GenerateTokenSettings.Issuer, _appSettings.GenerateTokenSettings.Audience);

                _helpers.SetTokenCookiesToResponse(generatedAccessToken, generatedRefreshToken, _appSettings.CookieSettings.AccessTokenCookieKey, _appSettings.CookieSettings.RefreshTokenCookieKey, _appSettings.GenerateTokenSettings.AccessTokenExpireTime, _appSettings.GenerateTokenSettings.RefreshTokenExpireTime);

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}