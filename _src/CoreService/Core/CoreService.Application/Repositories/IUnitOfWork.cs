using CoreService.Application.Repositories.CompanyRepository;
using CoreService.Application.Repositories.ProfileRepository;
using CoreService.Application.Repositories.RoleRepository;
using CoreService.Application.Repositories.ScreenRepository;
using CoreService.Application.Repositories.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Application.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();

        Task SaveChangesAsync(CancellationToken cancellationToken);

        IRoleReadRepository RoleReadRepository { get; }
        IRoleWriteRepository RoleWriteRepository { get; }
        IUserReadRepository UserReadRepository { get; }
        IUserWriteRepository UserWriteRepository { get; }
        IProfileReadRepository ProfileReadRepository { get; }
        IProfileWriteRepository ProfileWriteRepository { get; }
        ICompanyReadRepository CompanyReadRepository { get; }
        ICompanyWriteRepository CompanyWriteRepository { get; }
        IScreenReadRepository ScreenReadRepository { get; }
        IScreenWriteRepository ScreenWriteRepository { get; }
    }
}