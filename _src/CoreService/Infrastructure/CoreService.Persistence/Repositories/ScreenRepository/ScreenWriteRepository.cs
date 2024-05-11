using CoreService.Application.Contexts;
using CoreService.Application.Repositories.ScreenRepository;
using CoreService.Domain.AggregateRoots.User;
using CoreService.Persistence.Repositories.GenericRepository;
using System;

namespace CoreService.Persistence.Repositories.ScreenRepository
{
    internal class ScreenWriteRepository(MamboCoreDbContext dbContext) : GenericWriteRepository<ScreenEntity>(dbContext), IScreenWriteRepository
    {
    }
}