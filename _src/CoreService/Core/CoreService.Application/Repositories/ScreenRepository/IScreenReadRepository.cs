using CoreService.Application.Repositories.GenericRepository;
using CoreService.Domain.AggregateRoots.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Application.Repositories.ScreenRepository
{
    public interface IScreenReadRepository : IGenericReadRepository<ScreenEntity>
    {
        Task<HashSet<string>> GetOnlyScreenNamesAsNoTrackingAsync(CancellationToken cancellationToken);
    }
}