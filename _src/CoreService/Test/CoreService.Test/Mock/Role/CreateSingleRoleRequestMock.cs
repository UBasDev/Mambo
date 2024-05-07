using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Test.Mock.Role
{
    internal sealed class CreateSingleRoleRequestMock
    {
        public CreateSingleRoleRequestMock()
        {
            Name = string.Empty;
            ShortCode = string.Empty;
            Level = 0;
            Description = null;
        }

        public string Name { get; set; }
        public string ShortCode { get; set; }
        public UInt16 Level { get; set; }

        public string? Description { get; set; }
    }
}