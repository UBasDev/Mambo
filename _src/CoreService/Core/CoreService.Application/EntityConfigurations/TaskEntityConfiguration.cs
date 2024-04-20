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
    public class TaskEntityConfiguration : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasOne(t => t.TaskContent).WithOne(tc => tc.Task).HasForeignKey<TaskContentEntity>(tc => tc.TaskId);
            builder.HasOne(t => t.TaskDetail).WithOne(td => td.Task).HasForeignKey<TaskDetailEntity>(td => td.TaskId);
            builder.HasMany(t => t.TaskFields).WithOne(tf => tf.Task);
            builder.Property(t => t.No).IsRequired();
        }
    }
}