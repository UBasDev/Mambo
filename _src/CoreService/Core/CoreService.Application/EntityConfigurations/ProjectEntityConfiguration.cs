using CoreService.Domain.AggregateRoots.Project;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Application.EntityConfigurations
{
    public class ProjectEntityConfiguration : IEntityTypeConfiguration<ProjectEntity>
    {
        public void Configure(EntityTypeBuilder<ProjectEntity> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.Name).IsUnique();
            builder.HasMany(p => p.Tasks).WithOne(t => t.Project);
            builder.Property(p => p.Name).HasColumnType("varchar(100)").HasMaxLength(100).IsRequired();
        }
    }
}