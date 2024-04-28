using CoreService.Domain.AggregateRoots.User;
using Mambo.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Application.Features.Queries.Role.GetSingleRoleById
{
    public class GetSingleRoleByIdQueryResponse : BaseResponse<GetSingleRoleByIdQueryResponseModel?>
    {
    }

    public struct GetSingleRoleByIdQueryResponseModel
    {
        public GetSingleRoleByIdQueryResponseModel()
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

        private GetSingleRoleByIdQueryResponseModel(Guid id, string name, string shortCode, UInt16 level, string? description, DateTimeOffset? createdAt, DateTimeOffset? deletedAt, bool isActive, bool isDeleted, DateTimeOffset? updatedAt)
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
        public DateTimeOffset? CreatedAt { get; private set; }
        public DateTimeOffset? DeletedAt { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }

        public static GetSingleRoleByIdQueryResponseModel CreateNewGetSingleRoleByIdQueryResponseModel(Guid id, string name, string shortCode, UInt16 level, string? description, DateTimeOffset? createdAt, DateTimeOffset? deletedAt, bool isActive, bool isDeleted, DateTimeOffset? updatedAt)
        {
            return new GetSingleRoleByIdQueryResponseModel(id, name, shortCode, level, description, createdAt, deletedAt, isActive, isDeleted, updatedAt);
        }
    }
}