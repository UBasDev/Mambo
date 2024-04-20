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
    public class CompanyEntityConfiguration : IEntityTypeConfiguration<CompanyEntity>
    {
        public void Configure(EntityTypeBuilder<CompanyEntity> builder)
        {
            builder.HasIndex(c => c.Name).IsUnique();
            builder.HasMany(c => c.Profiles).WithOne(p => p.Company);
            builder.Property(c => c.Name).HasColumnType("varchar(100)").HasMaxLength(100).IsRequired();
            builder.Property(c => c.Description).HasColumnType("varchar(250)").HasMaxLength(100);
            builder.Property(c => c.Adress).HasColumnType("varchar(100)").HasMaxLength(100);
        }
    }
}