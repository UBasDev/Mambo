using CoreService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Domain.AggregateRoots.Project
{
    public sealed class TaskContentCommentEntity : BaseEntity<UInt64>, ISoftDelete
    {
        public TaskContentCommentEntity()
        {
            CommenterId = Guid.Empty;
            CommenterName = string.Empty;
            Description = string.Empty;
            TaskContent = null;
            TaskContentId = null;
            DeletedAt = null;
            IsActive = true;
            IsDeleted = false;
            UpdatedAt = null;
        }

        public Guid CommenterId { get; private set; }
        public string CommenterName { get; private set; }
        public string Description { get; private set; }
        public TaskContentEntity? TaskContent { get; private set; }
        public UInt64? TaskContentId { get; private set; }
        public DateTimeOffset? DeletedAt { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }
    }
}