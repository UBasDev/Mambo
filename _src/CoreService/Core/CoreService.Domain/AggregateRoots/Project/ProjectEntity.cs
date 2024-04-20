using CoreService.Domain.AggregateRoots.User;
using CoreService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Domain.AggregateRoots.Project
{
    public sealed class ProjectEntity : BaseEntity<Guid>, ISoftDelete
    {
        public ProjectEntity()
        {
            Name = string.Empty;
            Tasks = new HashSet<TaskEntity>();
            Users = new HashSet<UserEntity>();
            DeletedAt = null;
            IsActive = true;
            IsDeleted = false;
            UpdatedAt = null;
        }

        public string Name { get; private set; }
        public ICollection<TaskEntity> Tasks { get; private set; }
        public ICollection<UserEntity> Users { get; private set; }
        public DateTimeOffset? DeletedAt { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }
    }
}