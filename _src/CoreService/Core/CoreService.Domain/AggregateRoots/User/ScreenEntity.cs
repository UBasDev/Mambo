using CoreService.Domain.AggregateRoots.Project;
using CoreService.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Domain.AggregateRoots.User
{
    public sealed class ScreenEntity : BaseEntity<Guid>, ISoftDelete
    {
        public ScreenEntity()
        {
            Name = string.Empty;
            Value = string.Empty;
            OrderNumber = 0;
            Users = new HashSet<UserEntity>();
            Roles = new HashSet<RoleEntity>();
            DeletedAt = null;
            IsActive = true;
            IsDeleted = false;
            UpdatedAt = null;
        }

        private ScreenEntity(string name, string value, UInt16 orderNumber)
        {
            Name = name;
            Value = value;
            OrderNumber = orderNumber;
            Users = new HashSet<UserEntity>();
            Roles = new HashSet<RoleEntity>();
            DeletedAt = null;
            IsActive = true;
            IsDeleted = false;
            UpdatedAt = null;
        }

        public string Name { get; private set; }
        public string Value { get; private set; }
        public UInt16 OrderNumber { get; private set; }
        public ICollection<UserEntity> Users { get; private set; }
        public ICollection<RoleEntity> Roles { get; private set; }
        public DateTimeOffset? DeletedAt { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }

        public static (ScreenEntity? screenEntity, string? errorMessage) CreateNewScreenEntity(string name, string value, UInt16 orderNumber)
        {
            if (string.IsNullOrEmpty(name)) return (null, "Name field can't be empty");
            else if (string.IsNullOrEmpty(value)) return (null, "Value field can't be empty");
            else if (orderNumber == 0) return (null, "OrderNumber field can't be smaller than 1");
            return (new ScreenEntity(name, value, orderNumber), null);
        }
    }
}