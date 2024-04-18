using CoreService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Domain.AggregateRoots.Project
{
    public sealed class TaskContentEntity : BaseEntity<UInt64>
    {
        public TaskContentEntity()
        {
            Title = string.Empty;
            Description = string.Empty;
            Task = null;
            TaskId = null;
            TaskContentComments = new HashSet<TaskContentCommentEntity>();
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public TaskEntity? Task { get; private set; }
        public Guid? TaskId { get; private set; }
        public ICollection<TaskContentCommentEntity> TaskContentComments { get; private set; }
    }
}