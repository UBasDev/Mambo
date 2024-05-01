using CoreService.Application.Contexts;
using CoreService.Application.Repositories.CompanyRepository;
using CoreService.Domain.AggregateRoots.User;
using CoreService.Persistence.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Persistence.Repositories.CompanyRepository
{
    public class CompanyWriteRepository(MamboCoreDbContext dbContext) : GenericWriteRepository<CompanyEntity>(dbContext), ICompanyWriteRepository
    {
    }
}