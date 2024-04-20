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
        public DbSet<ProjectEntity> Projects => Set<ProjectEntity>();
        public DbSet<TaskContentCommentEntity> TaskContentComments => Set<TaskContentCommentEntity>();
        public DbSet<TaskContentEntity> TaskContents => Set<TaskContentEntity>();
        public DbSet<TaskDetailEntity> TaskDetails => Set<TaskDetailEntity>();
        public DbSet<TaskEntity> Tasks => Set<TaskEntity>();
        public DbSet<TaskFieldEntity> TaskFields => Set<TaskFieldEntity>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
        {
            return base.Remove(entity);
        }

        public override void RemoveRange(IEnumerable<object> entities)
        {
            base.RemoveRange(entities);
        }
    }
}