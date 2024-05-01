using CoreService.Domain.AggregateRoots.Project;
using CoreService.Domain.Common;
using CoreService.Domain.DomainEvents.User;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace CoreService.Domain.AggregateRoots.User
{
    public sealed partial class UserEntity : BaseEntity<Guid>, IAggregateRoot, ISoftDelete
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
            Screens = new HashSet<ScreenEntity>();
            DeletedAt = null;
            IsActive = true;
            IsDeleted = false;
            UpdatedAt = null;
        }

        private UserEntity(string username, string email, string passwordFromRequest)
        {
            Username = username;
            Email = email;
            var salt = GenerateSalt();
            PasswordSalt = Convert.ToBase64String(salt);
            PasswordHash = ComputeHash(passwordFromRequest, PasswordSalt);
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
        public ICollection<ScreenEntity> Screens { get; private set; }
        public DateTimeOffset? DeletedAt { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }
        private static readonly Regex passwordHasNumberRegex = CompiledPasswordHasNumberRegex();
        private static readonly Regex passwordHasUpperCharacterRegex = CompiledPasswordHasUpperCharacterRegex();
        private static readonly Regex passwordHasMinimumCharacterRegex = CompiledPasswordHasMinimumCharacterRegex();

        public static (UserEntity? userEntity, string? errorMessage) CreateNewUserEntity(string username, string email, string password)
        {
            if (string.IsNullOrEmpty(username)) return (null, "Username can't be empty");
            else if (string.IsNullOrEmpty(email)) return (null, "Email can't be empty");
            else if (string.IsNullOrEmpty(password)) return (null, "Password can't be empty");
            else if (!passwordHasNumberRegex.IsMatch(password) || !passwordHasUpperCharacterRegex.IsMatch(password) || !passwordHasMinimumCharacterRegex.IsMatch(password)) return (null, "Password must contain at least 1 numeric and 1 upper character and should be longer than 10 characters");
            return (new UserEntity(username, email, password), null);
        }

        public string? SetProfileAfterUserCreated(Guid userId, string firstname, string lastname, string companyName)
        {
            if (string.IsNullOrEmpty(userId.ToString())) return "User id can't be empty to create profile";
            else if (string.IsNullOrEmpty(firstname)) return "Firstname can't be empty to create profile";
            else if (string.IsNullOrEmpty(lastname)) return "Lastname can't be empty to create profile";
            else if (string.IsNullOrEmpty(companyName)) return "Company name can't be empty to create profile";
            AddDomainEvents(new SetProfileAfterUserCreatedEvent(userId, firstname, lastname, companyName));
            return null;
        }

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

        [GeneratedRegex(@"[0-9]+")] //Will be generated at compile time
        private static partial Regex CompiledPasswordHasNumberRegex();

        [GeneratedRegex(@"[A-Z]+")]
        private static partial Regex CompiledPasswordHasUpperCharacterRegex();

        [GeneratedRegex(@".{10,}")]
        private static partial Regex CompiledPasswordHasMinimumCharacterRegex();
    }
}