using CoreService.Application.Contexts;
using CoreService.Application.DTOs;
using CoreService.Application.Repositories;
using CoreService.Application.Repositories.UserRepository;
using CoreService.Domain.AggregateRoots.User;
using CoreService.Persistence.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace CoreService.Persistence.Repositories.UserRepository
{
    public class UserReadRepository(MamboCoreDbContext dbContext) : GenericReadRepository<UserEntity>(dbContext), IUserReadRepository
    {
        public async Task<UserSignInDto?> GetUserWithAllIncludesAsNoTrackingAsync(string username, CancellationToken cancellationToken)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .Where(u => u.Username == username)
                .Include(u => u.Profile).ThenInclude(p => p.Company)
                .Include(u => u.Role)
                .Select(u => UserSignInDto.CreateSignInDto(u.Id.ToString(), u.Username, u.Email, u.PasswordHash, u.PasswordSalt, (u.Profile == null ? null : u.Profile.Firstname), (u.Profile == null ? null : u.Profile.Lastname), (u.Profile == null || u.Profile.Company == null ? null : u.Profile.Company.Name), (u.Role == null ? null : u.Role.Name), (u.Role == null ? null : u.Role.Level), u.Screens.Select(s => s.Name).ToHashSet()))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<AdminSignInDto?> GetAdminUserWithAllIncludesAsNoTrackingAsync(string username, CancellationToken cancellationToken)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .Where(u => u.Username == username)
                .Include(u => u.Profile).ThenInclude(p => p.Company)
                .Include(u => u.Role)
                .Select(u => AdminSignInDto.CreateSignInDto(u.Id.ToString(), u.Username, u.Email, (u.Profile == null ? null : u.Profile.Firstname), (u.Profile == null ? null : u.Profile.Lastname), (u.Profile == null || u.Profile.Company == null ? null : u.Profile.Company.Name), (u.Role == null ? null : u.Role.Name), (u.Role == null ? null : u.Role.Level)))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<RefreshMyTokenDto?> GetOnlyTokenFieldsAsNoTrackingAsync(string username, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.Where(u => u.Username == username).Select(u => RefreshMyTokenDto.CreateRefreshMyToken(u.Username, u.Email, (u.Role == null ? null : u.Role.Name), (u.Role == null ? null : u.Role.Level))).FirstOrDefaultAsync(cancellationToken);
        }
    }
}