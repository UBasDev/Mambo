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
            Id = Guid.Empty;
            Username = string.Empty;
            Email = string.Empty;
            Firstname = string.Empty;
            Lastname = string.Empty;
            CompanyName = string.Empty;
        }

        private SignInCommandResponseModel(Guid id, string username, string email, string? firstname, string? lastname, string? companyName)
        {
            Id = id;
            Username = username;
            Email = email;
            Firstname = firstname;
            Lastname = lastname;
            CompanyName = companyName;
        }

        public Guid Id { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string? Firstname { get; private set; }
        public string? Lastname { get; private set; }
        public string? CompanyName { get; private set; }

        public static SignInCommandResponseModel CreateNewSignInCommandResponseModel(Guid id, string username, string email, string? firstname, string? lastname, string? companyName) => new SignInCommandResponseModel(id, username, email, firstname, lastname, companyName);
    }
}