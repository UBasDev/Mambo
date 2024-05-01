using CoreService.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace CoreService.Domain.AggregateRoots.User
{
    public sealed class ProfileEntity : BaseEntity<Guid>, ISoftDelete
    {
        public ProfileEntity()
        {
            Age = null;
            Firstname = string.Empty;
            Lastname = string.Empty;
            BirthDate = null;
            User = null;
            UserId = null;
            Company = null;
            CompanyId = null;
            DeletedAt = null;
            IsActive = true;
            IsDeleted = false;
            UpdatedAt = null;
        }

        private ProfileEntity(string firstname, string lastname, Guid userId)
        {
            Firstname = firstname;
            Lastname = lastname;
            UserId = userId;
            Age = null;
            BirthDate = null;
            User = null;
            Company = null;
            CompanyId = null;
            DeletedAt = null;
            IsActive = true;
            IsDeleted = false;
            UpdatedAt = null;
        }

        [Range(1, 100, ErrorMessage = "Age should be between 1 and 100")]
        public UInt16? Age { get; private set; }

        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public DateTimeOffset? BirthDate { get; private set; }
        public UserEntity? User { get; private set; }
        public Guid? UserId { get; private set; }
        public CompanyEntity? Company { get; private set; }
        public Guid? CompanyId { get; private set; }
        public DateTimeOffset? DeletedAt { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }

        public static (ProfileEntity? profileEntity, string? errorMessage) CreateNewProfileEntityWithoutAgeAndBirthDate(string firstname, string lastname, Guid userId)
        {
            if (string.IsNullOrEmpty(firstname)) return (null, "Firstname can't be empty");
            if (string.IsNullOrEmpty(lastname)) return (null, "Lastname can't be empty");
            if (string.IsNullOrEmpty(userId.ToString())) return (null, "User id can't be empty");
            return (new ProfileEntity(firstname, lastname, userId), null);
        }

        public void SetCompanyId(Guid companyId) => CompanyId = companyId;
    }
}