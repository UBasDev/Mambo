using Mambo.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Test.Mock.User
{
    internal sealed class SignInResponseMock : BaseResponseMock<SignInResponseMockModel>
    {
    }

    internal class SignInResponseMockModel
    {
        public SignInResponseMockModel()
        {
            Id = Guid.Empty;
            Username = string.Empty;
            Email = string.Empty;
            Firstname = string.Empty;
            Lastname = string.Empty;
            CompanyName = string.Empty;
        }

        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? CompanyName { get; set; }
    }
}