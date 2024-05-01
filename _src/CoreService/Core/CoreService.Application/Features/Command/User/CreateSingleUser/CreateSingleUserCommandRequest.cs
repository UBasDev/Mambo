using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Application.Features.Command.User.CreateSingleUser
{
    public class CreateSingleUserCommandRequest : IRequest<CreateSingleUserCommandResponse>
    {
        public CreateSingleUserCommandRequest()
        {
            Username = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            Firstname = string.Empty;
            Lastname = string.Empty;
            CompanyName = string.Empty;
        }

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string CompanyName { get; set; }
    }
}