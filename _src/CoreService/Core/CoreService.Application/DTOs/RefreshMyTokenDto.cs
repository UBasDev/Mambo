using CoreService.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Application.DTOs
{
    public sealed class RefreshMyTokenDto : BaseTokenGenerator
    {
        public RefreshMyTokenDto(Guid id, string? firstname, string? lastname, string? companyName, ICollection<string> screens, string username, string email, string? roleName, UInt16? roleLevel)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
            CompanyName = companyName;
            Screens = screens;
            Username = username;
            Email = email;
            RoleName = roleName;
            RoleLevel = roleLevel;
        }

        public Guid Id { get; private set; }
        public string? Firstname { get; private set; }
        public string? Lastname { get; private set; }
        public string? CompanyName { get; private set; }
        public ICollection<string> Screens { get; private set; }

        public static RefreshMyTokenDto CreateRefreshMyToken(Guid id, string? firstname, string? lastname, string? companyName, ICollection<string> screens, string username, string email, string? roleName, UInt16? roleLevel)
        {
            return new RefreshMyTokenDto(id, firstname, lastname, companyName, screens, username, email, roleName, roleLevel);
        }

        public void SetScreensRange(ICollection<string> screens)
        {
            if (screens.Count == 0) return;
            Screens = screens;
        }
    }
}