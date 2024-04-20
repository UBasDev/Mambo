using CoreService.Domain.Common;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Domain.MongoEntities
{
    public sealed class TaskContentCommentMediaEntity : BaseEntity<ObjectId>, ISoftDelete
    {
        public TaskContentCommentMediaEntity()
        {
            TaskContentCommentId = 0;
            MediaPath = string.Empty;
            MediaExtension = string.Empty;
            MediaLength = 0;
            DeletedAt = null;
            IsActive = true;
            IsDeleted = false;
            UpdatedAt = null;
        }

        public ulong TaskContentCommentId { get; private set; }
        public string MediaPath { get; private set; }
        public string MediaExtension { get; private set; }
        public uint MediaLength { get; private set; }
        public DateTimeOffset? DeletedAt { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }
    }
}