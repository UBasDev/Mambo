using CoreService.Domain.Common;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Domain.AggregateRoots.Project
{
    public sealed class TaskContentMediaEntity : BaseEntity<ObjectId>, ISoftDelete
    {
        public TaskContentMediaEntity()
        {
            TaskContentId = 0;
            MediaPath = string.Empty;
            MediaExtension = string.Empty;
            MediaLength = 0;
            DeletedAt = null;
            IsActive = true;
            IsDeleted = false;
            UpdatedAt = null;
        }

        public UInt64 TaskContentId { get; private set; }
        public string MediaPath { get; private set; }
        public string MediaExtension { get; private set; }
        public UInt32 MediaLength { get; private set; }
        public DateTimeOffset? DeletedAt { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }
    }
}