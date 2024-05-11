using CoreService.Application.Contexts;
using CoreService.Application.Repositories.ScreenRepository;
using CoreService.Domain.AggregateRoots.User;
using CoreService.Persistence.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Persistence.Repositories.ScreenRepository
{
    internal class ScreenReadRepository(MamboCoreDbContext _dbContext) : GenericReadRepository<ScreenEntity>(_dbContext), IScreenReadRepository
    {
        public async Task<HashSet<string>> GetOnlyScreenNamesAsNoTrackingAsync(CancellationToken cancellationToken)
        {
            var screenNames = new HashSet<string>();
            await foreach (var item in _dbContext.Screens.AsAsyncEnumerable().WithCancellation(cancellationToken))
            {
                screenNames.Add(item.Name);
            }
            return screenNames;
        }
    }
}