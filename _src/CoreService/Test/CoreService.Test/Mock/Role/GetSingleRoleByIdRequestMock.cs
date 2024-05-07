using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Test.Mock.Role
{
    internal sealed class GetSingleRoleByIdRequestMock
    {
        public GetSingleRoleByIdRequestMock()
        {
            Id = Guid.Empty;
        }

        public Guid Id { get; set; }
    }
}