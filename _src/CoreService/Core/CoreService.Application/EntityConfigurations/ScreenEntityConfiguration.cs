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
    public class ScreenEntityConfiguration : IEntityTypeConfiguration<ScreenEntity>
    {
        public void Configure(EntityTypeBuilder<ScreenEntity> builder)
        {
            builder.HasKey(s => s.Id);
            builder.HasIndex(s => s.Name).IsUnique();
            builder.HasIndex(s => s.Value).IsUnique();
            builder.HasIndex(s => s.OrderNumber).IsUnique();
            builder.Property(r => r.Name).HasColumnType("varchar(30)").HasMaxLength(30).IsRequired();
            builder.Property(r => r.Value).HasColumnType("varchar(30)").HasMaxLength(30).IsRequired();
            builder.Property(r => r.OrderNumber).HasColumnType("smallint").IsRequired();
        }
    }
}