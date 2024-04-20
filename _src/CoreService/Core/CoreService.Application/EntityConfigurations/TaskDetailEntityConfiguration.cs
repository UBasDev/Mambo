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
    public class TaskDetailEntityConfiguration : IEntityTypeConfiguration<TaskDetailEntity>
    {
        public void Configure(EntityTypeBuilder<TaskDetailEntity> builder)
        {
            builder.HasKey(td => td.Id);
            builder.Property(td => td.AssignedUserId).IsRequired();
            builder.Property(td => td.ReporterUserId).IsRequired();
            builder.Property(td => td.PriorityList).HasColumnType("varchar(250)").IsRequired();
            builder.Property(td => td.CurrentPriority).HasColumnType("varchar(50)").IsRequired();
            builder.Property(td => td.StatusList).HasColumnType("varchar(500)").IsRequired();
            builder.Property(td => td.CurrentStatus).HasColumnType("varchar(50)").IsRequired();
            builder.Property(td => td.RelatedTaskIdList).HasColumnType("varchar(500)");
        }
    }
}