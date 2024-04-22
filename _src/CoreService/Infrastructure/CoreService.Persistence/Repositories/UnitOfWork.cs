using CoreService.Application.Contexts;
using CoreService.Application.Repositories;
using CoreService.Application.Repositories.RoleRepository;
using CoreService.Persistence.Repositories.RoleRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Persistence.Repositories
{
    public class UnitOfWork(MamboCoreDbContext dbContext) : IUnitOfWork
    {
        private readonly MamboCoreDbContext _dbContext = dbContext;

        public IRoleReadRepository RoleReadRepository
        {
            get
            {
                return new RoleReadRepository(_dbContext);
            }
        }

        public IRoleWriteRepository RoleWriteRepository
        {
            get
            {
                return new RoleWriteRepository(_dbContext);
            }
        }

        public void SaveChanges() => _dbContext.SaveChanges();

        public async Task SaveChangesAsync(CancellationToken cancellationToken) => await _dbContext.SaveChangesAsync(cancellationToken);

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed && disposing) _dbContext.Dispose();
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}