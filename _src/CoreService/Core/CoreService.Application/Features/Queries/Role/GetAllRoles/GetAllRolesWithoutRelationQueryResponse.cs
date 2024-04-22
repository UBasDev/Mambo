using Mambo.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Application.Features.Queries.Role.GetAllRoles
{
    public class GetAllRolesWithoutRelationQueryResponse : BaseResponse<List<GetAllRolesQueryResponseModel>>
    {
    }

    public struct GetAllRolesQueryResponseModel
    {
        public GetAllRolesQueryResponseModel()
        {
            Id = Guid.Empty;
            Name = string.Empty;
            ShortCode = string.Empty;
            Level = 0;
            Description = null;
            CreatedAt = DateTimeOffset.UtcNow;
            DeletedAt = null;
            IsActive = true;
            IsDeleted = false;
            UpdatedAt = null;
        }

        public GetAllRolesQueryResponseModel(Guid id, string name, string shortCode, UInt16 level, string? description, DateTimeOffset createdAt, DateTimeOffset? deletedAt, bool isActive, bool isDeleted, DateTimeOffset? updatedAt)
        {
            Id = id;
            Name = name;
            ShortCode = shortCode;
            Level = level;
            Description = description;
            CreatedAt = createdAt;
            DeletedAt = deletedAt;
            IsActive = isActive;
            IsDeleted = isDeleted;
            UpdatedAt = updatedAt;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string ShortCode { get; private set; }
        public UInt16 Level { get; private set; }
        public string? Description { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? DeletedAt { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }

        public static GetAllRolesQueryResponseModel CreateNewGetAllRolesQueryResponse(Guid id, string name, string shortCode, UInt16 level, string? description, DateTimeOffset createdAt, DateTimeOffset? deletedAt, bool isActive, bool isDeleted, DateTimeOffset? updatedAt)
        {
            return new GetAllRolesQueryResponseModel(id, name, shortCode, level, description, createdAt, deletedAt, isActive, isDeleted, updatedAt);
        }
    }
}