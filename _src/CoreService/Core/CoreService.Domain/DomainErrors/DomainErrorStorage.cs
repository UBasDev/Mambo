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
            public static readonly string NotFound = "This user not found";
            public static readonly string InvalidRoles = "Your role is not allowed";
            public static readonly string DuplicateUsername = "This username is already in use";
        }

        public static class RoleErrors
        {
            public static readonly string LevelNotValid = "Role level should be between 1 and 5000";
            public static readonly string NameNotValid = "Role name can't be empty";
            public static readonly string ShortCodeNotValid = "Role shortCode can't be empty";
        }
    }
}