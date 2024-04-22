using CoreService.Application.Contexts;
using CoreService.Application.Repositories.RoleRepository;
using CoreService.Domain.AggregateRoots.User;
using CoreService.Persistence.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Persistence.Repositories.RoleRepository
{
    public class RoleWriteRepository(MamboCoreDbContext _dbContext) : GenericWriteRepository<RoleEntity>(_dbContext), IRoleWriteRepository
    {
    }
}