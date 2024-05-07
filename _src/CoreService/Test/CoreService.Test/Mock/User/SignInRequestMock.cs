using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Test.Mock.User
{
    internal sealed class SignInRequestMock
    {
        public SignInRequestMock()
        {
            EmailOrUsername = string.Empty;
            Password = string.Empty;
        }
        public string EmailOrUsername { get; set; }
        public string Password { get; set; }
    }
}