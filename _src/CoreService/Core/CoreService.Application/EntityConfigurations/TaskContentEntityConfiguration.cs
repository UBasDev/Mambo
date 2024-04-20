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
    public class TaskContentEntityConfiguration : IEntityTypeConfiguration<TaskContentEntity>
    {
        public void Configure(EntityTypeBuilder<TaskContentEntity> builder)
        {
            builder.HasKey(tc => tc.Id);
            builder.HasMany(tc => tc.TaskContentComments).WithOne(tcc => tcc.TaskContent);
            builder.Property(tc => tc.Title).HasColumnType("varchar(100)").IsRequired();
            builder.Property(tc => tc.Description).HasColumnType("text").IsRequired();
        }
    }
}