using CoreService.Application.Contexts;
using CoreService.Application.Repositories.UserRepository;
using CoreService.Domain.AggregateRoots.User;
using CoreService.Persistence.Repositories.GenericRepository;

namespace CoreService.Persistence.Repositories.UserRepository
{
    public class UserReadRepository(MamboCoreDbContext dbContext) : GenericReadRepository<UserEntity>(dbContext), IUserReadRepository
    {
    }
}