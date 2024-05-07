using Mambo.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Test.Mock.Role
{
    internal sealed class GetSingleRoleByIdResponseMock : BaseResponseMock<GetSingleRoleByIdResponseMockModel>
    {
    }

    public class GetSingleRoleByIdResponseMockModel
    {
        public GetSingleRoleByIdResponseMockModel()
        {
            Id = Guid.Empty;
            Name = string.Empty;
            ShortCode = string.Empty;
            Level = 0;
            Description = string.Empty;
            CreatedAt = null;
            DeletedAt = null;
            IsActive = false;
            IsDeleted = false;
            UpdatedAt = null;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortCode { get; set; }
        public UInt16 Level { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}