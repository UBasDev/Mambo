using CoreService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Domain.AggregateRoots.Project
{
    public sealed class TaskFieldEntity : BaseEntity<UInt64>
    {
        public TaskFieldEntity()
        {
            Name = string.Empty;
            Value = string.Empty;
            Task = null;
            TaskId = null;
        }

        public string Name { get; private set; }
        public string Value { get; private set; }
        public TaskEntity? Task { get; private set; }
        public Guid? TaskId { get; private set; }
    }
}