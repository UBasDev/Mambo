using CoreService.Domain.Common;

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

        public string Name { get; private set; }
        public string? Description { get; private set; }
        public string? Adress { get; private set; }
        public ICollection<ProfileEntity> Profiles { get; private set; }
        public DateTimeOffset? DeletedAt { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }
    }
}