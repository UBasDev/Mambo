using CoreService.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace CoreService.Domain.AggregateRoots.User
{
    public sealed class ProfileEntity : BaseEntity<Guid>, ISoftDelete
    {
        public ProfileEntity()
        {
            Age = 0;
            Firstname = string.Empty;
            Lastname = string.Empty;
            BirthDate = DateTimeOffset.UtcNow;
            User = null;
            UserId = null;
            Company = null;
            CompanyId = null;
            DeletedAt = null;
            IsActive = true;
            IsDeleted = false;
            UpdatedAt = null;
        }

        [Range(1, 100, ErrorMessage = "Age should be between 1 and 100")]
        public UInt16 Age { get; private set; }

        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public DateTimeOffset BirthDate { get; private set; }
        public UserEntity? User { get; private set; }
        public Guid? UserId { get; private set; }
        public CompanyEntity? Company { get; private set; }
        public Guid? CompanyId { get; private set; }
        public DateTimeOffset? DeletedAt { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }
    }
}