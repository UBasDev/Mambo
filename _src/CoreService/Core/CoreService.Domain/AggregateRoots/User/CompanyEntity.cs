using CoreService.Domain.Common;
using System.Xml.Linq;

namespace CoreService.Domain.AggregateRoots.User
{
    public sealed class CompanyEntity : BaseEntity<Guid>, ISoftDelete
    {
        public CompanyEntity()
        {
            Name = string.Empty;
            Description = null;
            Adress = null;
            Profiles = new HashSet<ProfileEntity>();
            DeletedAt = null;
            IsActive = true;
            IsDeleted = false;
            UpdatedAt = null;
        }

        private CompanyEntity(string name)
        {
            Name = name;
            Description = null;
            Adress = null;
            Profiles = new HashSet<ProfileEntity>();
            DeletedAt = null;
            IsActive = true;
            IsDeleted = false;
            UpdatedAt = null;
        }

        public string Name { get; private set; }
        public string? Description { get; private set; }
        public string? Adress { get; private set; }
        public ICollection<ProfileEntity> Profiles { get; private set; }
        public DateTimeOffset? DeletedAt { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }

        public static (CompanyEntity? companyEntity, string? errorMessage) CreateNewCompanyOnlyWithName(string name)
        {
            if (string.IsNullOrEmpty(name)) return (null, "Company name can't be empty");
            return (new CompanyEntity(name), null);
        }
    }
}