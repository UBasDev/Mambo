using Mambo.Mongo.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Domain
{
    public sealed class SentMailEntity : BaseMongoEntity<ObjectId>
    {
        public SentMailEntity()
        {
            UserId = string.Empty;
            Email = string.Empty;
            Reason = string.Empty;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        private SentMailEntity(string userId, string email, string reason)
        {
            UserId = userId;
            Email = email;
            Reason = reason;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public string UserId { get; private set; }
        public string Email { get; private set; }
        public string Reason { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }

        public static SentMailEntity CreateSentMail(string userId, string email, string reason)
        {
            return new SentMailEntity(userId, email, reason);
        }
    }
}