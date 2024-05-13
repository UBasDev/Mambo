using CoreService.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Application.DTOs
{
    public sealed class UserSignInDto : BaseTokenGenerator
    {
        public UserSignInDto()
        {
            Id = string.Empty;
            Username = string.Empty;
            Email = string.Empty;
            PasswordHash = null;
            PasswordSalt = null;
            Firstname = null;
            Lastname = null;
            CompanyName = null;
            RoleName = null;
            RoleLevel = null;
            Screens = new HashSet<string>();
        }

        private UserSignInDto(string id, string username, string email, string? passwordHash, string? passwordSalt, string? firstname, string? lastname, string? companyName, string? roleName, UInt16? roleLevel, ICollection<string> screens)
        {
            Id = id;
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            Firstname = firstname;
            Lastname = lastname;
            CompanyName = companyName;
            RoleName = roleName;
            RoleLevel = roleLevel;
            Screens = screens;
        }

        public string Id { get; private set; }
        public string? Firstname { get; private set; }
        public string? Lastname { get; private set; }
        public string? PasswordHash { get; private set; }
        public string? PasswordSalt { get; private set; }
        public string? CompanyName { get; private set; }

        public ICollection<string> Screens { get; private set; }

        public static UserSignInDto CreateSignInDto(string id, string username, string email, string? passwordHash, string? passwordSalt, string? firstname, string? lastname, string? companyName, string? roleName, UInt16? roleLevel, ICollection<string> screens)
        {
            return new UserSignInDto(id, username, email, passwordHash, passwordSalt, firstname, lastname, companyName, roleName, roleLevel, screens);
        }
    }
}