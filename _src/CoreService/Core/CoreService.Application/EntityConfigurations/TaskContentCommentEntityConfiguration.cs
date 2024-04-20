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
    public class TaskContentCommentEntityConfiguration : IEntityTypeConfiguration<TaskContentCommentEntity>
    {
        public void Configure(EntityTypeBuilder<TaskContentCommentEntity> builder)
        {
            builder.HasKey(tcc => tcc.Id);
            builder.Property(tcc => tcc.CommenterId).IsRequired();
            builder.Property(tcc => tcc.CommenterName).HasColumnType("varchar(100)").HasMaxLength(50).IsRequired();
            builder.Property(tcc => tcc.Description).HasColumnType("text").HasMaxLength(100).IsRequired();
        }
    }
}