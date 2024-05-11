using CoreService.Domain.AggregateRoots.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Application.EntityConfigurations
{
    public class RoleEntityConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.HasKey(r => r.Id);
            builder.HasIndex(r => r.Name).IsUnique();
            builder.HasIndex(r => r.ShortCode).IsUnique();
            builder.HasIndex(r => r.Level).IsUnique();
            builder.HasMany(r => r.Users).WithOne(u => u.Role);
            builder.HasMany(r => r.Screens).WithMany(s => s.Roles).UsingEntity(join => join.ToTable("RoleScreen"));
            builder.Property(r => r.Name).HasColumnType("varchar(30)").HasMaxLength(30).IsRequired();
            builder.Property(r => r.ShortCode).HasColumnType("varchar(5)").HasMaxLength(5).IsRequired();
            builder.Property(r => r.Level).HasColumnType("smallint").IsRequired();
            builder.Property(r => r.Description).HasColumnType("varchar(100)").HasMaxLength(100);
        }
    }
}