using ProjectEntity = CoreService.Domain.AggregateRoots.Project.ProjectEntity;
using CoreService.Domain.Common;
using System.Security.Cryptography;

namespace CoreService.Domain.AggregateRoots.User
{
    public sealed class UserEntity : BaseEntity<Guid>, IAggregateRoot, ISoftDelete
    {
        public UserEntity()
        {
            Username = string.Empty;
            Email = string.Empty;
            PasswordSalt = string.Empty;
            PasswordHash = string.Empty;
            LastLoginDate = null;
            Role = null;
            RoleId = null;
            Profile = null;
            Projects = new HashSet<ProjectEntity>();
            DeletedAt = null;
            IsActive = true;
            IsDeleted = false;
            UpdatedAt = null;
        }

        public string Username { get; private set; }
        public string Email { get; private set; }
        public string PasswordSalt { get; private set; }
        public string PasswordHash { get; private set; }
        public DateTimeOffset? LastLoginDate { get; private set; }
        public RoleEntity? Role { get; private set; }
        public Guid? RoleId { get; private set; }
        public ProfileEntity? Profile { get; private set; }
        public ICollection<ProjectEntity> Projects { get; private set; }
        public DateTimeOffset? DeletedAt { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }

        private static byte[] GenerateSalt()
        {
            var rng = RandomNumberGenerator.Create();
            var salt = new byte[24];
            rng.GetBytes(salt);
            return salt;
        }

        public static string ComputeHash(string password, string saltString)
        {
            var salt = Convert.FromBase64String(saltString);
            using var hashGenerator = new Rfc2898DeriveBytes(password, salt, 10101, HashAlgorithmName.SHA512);
            var bytes = hashGenerator.GetBytes(24);
            return Convert.ToBase64String(bytes);
        }
    }
}