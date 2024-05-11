using CoreService.Application.Contexts;
using CoreService.Application.Repositories;
using CoreService.Application.Repositories.CompanyRepository;
using CoreService.Application.Repositories.ProfileRepository;
using CoreService.Application.Repositories.RoleRepository;
using CoreService.Application.Repositories.ScreenRepository;
using CoreService.Application.Repositories.UserRepository;
using CoreService.Persistence.Repositories.CompanyRepository;
using CoreService.Persistence.Repositories.ProfileRepository;
using CoreService.Persistence.Repositories.RoleRepository;
using CoreService.Persistence.Repositories.ScreenRepository;
using CoreService.Persistence.Repositories.UserRepository;

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

        public IUserReadRepository UserReadRepository
        {
            get
            {
                return new UserReadRepository(_dbContext);
            }
        }

        public IUserWriteRepository UserWriteRepository
        {
            get
            {
                return new UserWriteRepository(_dbContext);
            }
        }

        public IProfileReadRepository ProfileReadRepository
        {
            get
            {
                return new ProfileReadRepository(_dbContext);
            }
        }

        public IProfileWriteRepository ProfileWriteRepository
        {
            get
            {
                return new ProfileWriteRepository(_dbContext);
            }
        }

        public ICompanyReadRepository CompanyReadRepository
        {
            get
            {
                return new CompanyReadRepository(_dbContext);
            }
        }

        public ICompanyWriteRepository CompanyWriteRepository
        {
            get
            {
                return new CompanyWriteRepository(_dbContext);
            }
        }

        public IScreenReadRepository ScreenReadRepository
        {
            get
            {
                return new ScreenReadRepository(_dbContext);
            }
        }

        public IScreenWriteRepository ScreenWriteRepository
        {
            get
            {
                return new ScreenWriteRepository(_dbContext);
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