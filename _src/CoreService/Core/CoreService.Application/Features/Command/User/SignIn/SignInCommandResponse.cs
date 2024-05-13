using Mambo.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Application.Features.Command.User.SignIn
{
    public class SignInCommandResponse : BaseResponse<SignInCommandResponseModel?>
    {
    }

    public struct SignInCommandResponseModel
    {
        public SignInCommandResponseModel()
        {
            Username = string.Empty;
            Email = string.Empty;
            Firstname = null;
            Lastname = null;
            CompanyName = null;
            RoleName = null;
            RoleLevel = null;
            Screens = new HashSet<string>();
        }

        private SignInCommandResponseModel(string username, string email, string? firstname, string? lastname, string? companyName, string? roleName, UInt16? roleLevel, ICollection<string> screens)
        {
            Username = username;
            Email = email;
            Firstname = firstname;
            Lastname = lastname;
            CompanyName = companyName;
            RoleName = roleName;
            RoleLevel = roleLevel;
            Screens = screens;
        }

        public string Username { get; private set; }
        public string Email { get; private set; }
        public string? Firstname { get; private set; }
        public string? Lastname { get; private set; }
        public string? CompanyName { get; private set; }
        public string? RoleName { get; private set; }
        public UInt16? RoleLevel { get; private set; }
        public ICollection<string> Screens { get; set; }

        public static SignInCommandResponseModel CreateNewSignInCommandResponseModel(string username, string email, string? firstname, string? lastname, string? companyName, string? roleName, UInt16? roleLevel, ICollection<string> screens) => new SignInCommandResponseModel(username, email, firstname, lastname, companyName, roleName, roleLevel, screens);
    }
}