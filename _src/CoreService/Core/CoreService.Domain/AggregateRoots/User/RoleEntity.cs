using CoreService.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace CoreService.Domain.AggregateRoots.User
{
    public sealed class RoleEntity : BaseEntity<Guid>, ISoftDelete
    {
        public RoleEntity()
        {
            Name = string.Empty;
            ShortCode = string.Empty;
            Level = 0;
            Description = string.Empty;
            Users = new HashSet<UserEntity>();
            DeletedAt = null;
            IsActive = true;
            IsDeleted = false;
            UpdatedAt = null;
        }

        public string Name { get; private set; }
        public string ShortCode { get; private set; }

        [Range(1, 5000, ErrorMessage = "Role level should be between 1 and 5000")]
        public UInt16 Level { get; private set; }

        public string? Description { get; private set; }
        public ICollection<UserEntity> Users { get; private set; }
        public DateTimeOffset? DeletedAt { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }
    }
}