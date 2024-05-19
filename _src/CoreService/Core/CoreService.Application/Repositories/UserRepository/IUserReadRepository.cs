using CoreService.Application.DTOs;
using CoreService.Application.Repositories.GenericRepository;
using CoreService.Domain.AggregateRoots.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Application.Repositories.UserRepository
{
    public interface IUserReadRepository : IGenericReadRepository<UserEntity>
    {
        Task<UserSignInDto?> GetUserWithAllIncludesAsNoTrackingAsync(string username, CancellationToken cancellationToken);

        Task<AdminSignInDto?> GetAdminUserWithAllIncludesAsNoTrackingAsync(string username, CancellationToken cancellationToken);

        Task<RefreshMyTokenDto?> GetOnlyTokenFieldsAsNoTrackingAsync(string username, CancellationToken cancellationToken);
    }
}