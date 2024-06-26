﻿using CoreService.Application.EntityConfigurations;
using CoreService.Domain.AggregateRoots.Project;
using CoreService.Domain.AggregateRoots.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Application.Contexts
{
    public class MamboCoreDbContext(DbContextOptions<MamboCoreDbContext> options) : DbContext(options)
    {
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<ProfileEntity> Profiles => Set<ProfileEntity>();
        public DbSet<RoleEntity> Roles => Set<RoleEntity>();
        public DbSet<CompanyEntity> Companies => Set<CompanyEntity>();
        public DbSet<ScreenEntity> Screens => Set<ScreenEntity>();
        public DbSet<ProjectEntity> Projects => Set<ProjectEntity>();
        public DbSet<TaskContentCommentEntity> TaskContentComments => Set<TaskContentCommentEntity>();
        public DbSet<TaskContentEntity> TaskContents => Set<TaskContentEntity>();
        public DbSet<TaskDetailEntity> TaskDetails => Set<TaskDetailEntity>();
        public DbSet<TaskEntity> Tasks => Set<TaskEntity>();
        public DbSet<TaskFieldEntity> TaskFields => Set<TaskFieldEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProfileEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ScreenEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TaskContentCommentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TaskContentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TaskDetailEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TaskEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TaskFieldEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        }

        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public override EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
        {
            return base.Remove(entity);
        }

        public override void RemoveRange(IEnumerable<object> entities)
        {
            base.RemoveRange(entities);
        }
        */
    }
}