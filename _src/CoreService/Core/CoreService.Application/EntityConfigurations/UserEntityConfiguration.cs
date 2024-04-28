using CoreService.Domain.AggregateRoots.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Application.EntityConfigurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.Username);
            builder.HasIndex(u => u.Email);
            builder.HasOne(u => u.Profile).WithOne(p => p.User).HasForeignKey<ProfileEntity>(p => p.UserId);
            builder.HasMany(u => u.Projects).WithMany(p => p.Users);
            builder.HasMany(u => u.Screens).WithMany(s => s.Users);
            builder.Property(u => u.Username).HasColumnType("varchar(30)").HasMaxLength(30).IsRequired();
            builder.Property(u => u.Email).HasColumnType("varchar(50)").HasMaxLength(50).IsRequired();
            builder.Property(u => u.PasswordHash).IsRequired();
            builder.Property(u => u.PasswordSalt).IsRequired();
        }
    }
}