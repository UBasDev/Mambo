﻿using CoreService.Application.Contexts;
using CoreService.Application.Repositories.ProfileRepository;
using CoreService.Domain.AggregateRoots.User;
using CoreService.Persistence.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Persistence.Repositories.ProfileRepository
{
    public class ProfileReadRepository(MamboCoreDbContext dbContext) : GenericReadRepository<ProfileEntity>(dbContext), IProfileReadRepository
    {
    }
}