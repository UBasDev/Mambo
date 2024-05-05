using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Application.Features.Command.User.SignIn
{
    public class SignInCommandRequest : IRequest<SignInCommandResponse>
    {
        public SignInCommandRequest()
        {
            EmailOrUsername = string.Empty;
            Password = string.Empty;
        }

        public string EmailOrUsername { get; set; }
        public string Password { get; set; }
    }
}