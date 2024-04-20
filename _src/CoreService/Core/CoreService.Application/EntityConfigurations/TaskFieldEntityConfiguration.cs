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
    public class TaskFieldEntityConfiguration : IEntityTypeConfiguration<TaskFieldEntity>
    {
        public void Configure(EntityTypeBuilder<TaskFieldEntity> builder)
        {
            builder.HasKey(tf => tf.Id);
            builder.Property(tf => tf.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(tf => tf.Value).HasColumnType("varchar(50)").IsRequired();
        }
    }
}