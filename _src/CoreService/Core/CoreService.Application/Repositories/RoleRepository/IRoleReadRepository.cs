using CoreService.Application.Features.Queries.Role.GetAllRoles;
using CoreService.Application.Features.Queries.Role.GetSingleRoleById;
using CoreService.Application.Repositories.GenericRepository;
using CoreService.Domain.AggregateRoots.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Application.Repositories.RoleRepository
{
    public interface IRoleReadRepository : IGenericReadRepository<RoleEntity>
    {
        Task<List<GetAllRolesQueryResponseModel>> GetAllRolesWithoutRelationAsNoTrackingAsync();

        Task<GetSingleRoleByIdQueryResponseModel?> GetSingleRoleByIdAsNoTrackingAsync(Guid roleId, CancellationToken cancellationToken);
    }
}