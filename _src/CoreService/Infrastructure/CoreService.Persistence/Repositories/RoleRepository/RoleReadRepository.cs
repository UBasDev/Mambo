using CoreService.Application.Contexts;
using CoreService.Application.Features.Queries.Role.GetAllRoles;
using CoreService.Application.Features.Queries.Role.GetSingleRoleById;
using CoreService.Application.Repositories.RoleRepository;
using CoreService.Domain.AggregateRoots.User;
using CoreService.Persistence.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Persistence.Repositories.RoleRepository
{
    public class RoleReadRepository(MamboCoreDbContext dbContext) : GenericReadRepository<RoleEntity>(dbContext), IRoleReadRepository
    {
        private readonly MamboCoreDbContext _dbContext = dbContext;

        public async Task<List<GetAllRolesQueryResponseModel>> GetAllRolesWithoutRelationAsNoTrackingAsync()
        {
            return await _dbContext.Roles.AsNoTracking().Select(r => GetAllRolesQueryResponseModel.CreateNewGetAllRolesQueryResponse(r.Id, r.Name, r.ShortCode, r.Level, r.Description, r.CreatedAt, r.DeletedAt, r.IsActive, r.IsDeleted, r.UpdatedAt)).ToListAsync();
        }

        public async Task<GetSingleRoleByIdQueryResponseModel?> GetSingleRoleByIdAsNoTrackingAsync(Guid roleId, CancellationToken cancellationToken)
        {
            return await _dbContext.Roles.AsNoTracking().Where(r => r.Id == roleId).Select(r => GetSingleRoleByIdQueryResponseModel.CreateNewGetSingleRoleByIdQueryResponseModel(r.Id, r.Name, r.ShortCode, r.Level, r.Description, r.CreatedAt, r.DeletedAt, r.IsActive, r.IsDeleted, r.UpdatedAt)).Cast<GetSingleRoleByIdQueryResponseModel?>().FirstOrDefaultAsync(cancellationToken);
        }
    }
}