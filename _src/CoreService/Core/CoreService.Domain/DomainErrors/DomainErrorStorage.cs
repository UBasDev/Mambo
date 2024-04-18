using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Domain.DomainErrors
{
    public static class DomainErrorStorage
    {
        public static class UserErrors
        {
            public static readonly string NotFound = "This user not found.";
            public static readonly string InvalidRoles = "Your role is not allowed.";
            public static readonly string DuplicateUsername = "This username is already in use.";
        }
    }
}