using CoreService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Domain.AggregateRoots.Project
{
    public sealed class TaskDetailEntity : BaseEntity<UInt64>
    {
        public TaskDetailEntity()
        {
            AssignedUserId = Guid.Empty;
            ReporterUserId = Guid.Empty;
            CurrentPriority = string.Empty;
            PriorityList = string.Empty;
            CurrentStatus = string.Empty;
            StatusList = string.Empty;
            RelatedTaskIdList = string.Empty;
            Task = null;
            TaskId = null;
        }

        public Guid AssignedUserId { get; private set; }
        public Guid ReporterUserId { get; private set; }
        public string PriorityList { get; private set; }
        public string CurrentPriority { get; private set; }
        public string StatusList { get; private set; }
        public string CurrentStatus { get; private set; }
        public string? RelatedTaskIdList { get; private set; }
        public TaskEntity? Task { get; private set; }
        public Guid? TaskId { get; private set; }
    }
}