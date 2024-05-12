using Mambo.Mongo.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenService.Domain
{
    public sealed class TokenEntity : BaseMongoEntity<ObjectId>
    {
        public TokenEntity()
        {
            UserId = string.Empty;
            AccessToken = string.Empty;
            AccessTokenExpireDate = 0;
            RefreshToken = string.Empty;
            RefreshTokenExpireDate = 0;
        }

        private TokenEntity(string userId, string accessToken, UInt64 accessTokenExpireDate, string refreshToken, UInt64 refreshTokenExpireDate)
        {
            UserId = userId;
            AccessToken = accessToken;
            AccessTokenExpireDate = accessTokenExpireDate;
            RefreshToken = refreshToken;
            RefreshTokenExpireDate = refreshTokenExpireDate;
        }

        public string UserId { get; private set; }
        public string AccessToken { get; private set; }
        public UInt64 AccessTokenExpireDate { get; private set; }
        public string RefreshToken { get; private set; }
        public UInt64 RefreshTokenExpireDate { get; private set; }

        public static TokenEntity CreateNewTokenEntity(string userId, string accessToken, UInt64 accessTokenExpireDate, string refreshToken, UInt64 refreshTokenExpireDate)
        {
            return new TokenEntity(userId, accessToken, accessTokenExpireDate, refreshToken, refreshTokenExpireDate);
        }

        public string? UpdateCurrentToken(string accessToken, UInt64 accessTokenExpireDate, string refreshToken, UInt64 refreshTokenExpireDate)
        {
            if (!string.IsNullOrEmpty(accessToken)) return "Access token can't be null or empty";
            else if (accessTokenExpireDate == 0) return "Access token expire date can't be zero";
            else if (!string.IsNullOrEmpty(refreshToken)) return "Refresh token can't be null or empty";
            else if (refreshTokenExpireDate == 0) return "Refresh token expire date can't be zero";
            AccessToken = accessToken;
            AccessTokenExpireDate = accessTokenExpireDate;
            RefreshToken = refreshToken;
            RefreshTokenExpireDate = refreshTokenExpireDate;
            return null;
        }
    }
}