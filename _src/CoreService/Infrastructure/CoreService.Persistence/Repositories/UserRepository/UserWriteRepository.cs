﻿using CoreService.Application.Contexts;
using CoreService.Application.Repositories.UserRepository;
using CoreService.Domain.AggregateRoots.User;
using CoreService.Persistence.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Persistence.Repositories.UserRepository
{
    public class UserWriteRepository(MamboCoreDbContext dbContext) : GenericWriteRepository<UserEntity>(dbContext), IUserWriteRepository
    {
    }
}