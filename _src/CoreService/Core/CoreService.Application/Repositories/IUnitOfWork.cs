using CoreService.Application.Repositories.RoleRepository;
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
    }
}