using CoreService.Domain.Common;
using CoreService.Domain.DomainErrors;
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
            Screens = new HashSet<ScreenEntity>();
            DeletedAt = null;
            IsActive = true;
            IsDeleted = false;
            UpdatedAt = null;
        }

        public RoleEntity(string name, string shortCode, UInt16 level, string? description)
        {
            Name = name;
            ShortCode = shortCode;
            Level = level;
            Description = description;
            Users = new HashSet<UserEntity>();
            Screens = new HashSet<ScreenEntity>();
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
        public ICollection<ScreenEntity> Screens { get; private set; }
        public DateTimeOffset? DeletedAt { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }

        public static (RoleEntity? newRoleEntity, string? errorMessage) CreateNewRole(string name, string shortCode, UInt16 level, string? description)
        {
            if (string.IsNullOrEmpty(name)) return (null, DomainErrorStorage.RoleErrors.NameNotValid);
            else if (string.IsNullOrEmpty(shortCode)) return (null, DomainErrorStorage.RoleErrors.ShortCodeNotValid);
            else if (level < 1 || level > 5000) return (null, DomainErrorStorage.RoleErrors.LevelNotValid);
            return (new RoleEntity(name, shortCode, level, description), null);
        }

        public string? AddSingleScreenToCurrentRole(ScreenEntity screenToAdd)
        {
            if (screenToAdd == null) return "Screen can't be null";
            Screens.Add(screenToAdd);
            return null;
        }
    }
}