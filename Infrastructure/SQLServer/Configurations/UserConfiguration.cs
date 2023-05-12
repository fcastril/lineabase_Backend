using Domain.Common.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.SQLServer.Configurations
{
    public class UserConfiguration : BaseConfiguration<User>,IEntityTypeConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);


            builder.OwnsOne(p => p.Name)
                    .Property(p => p.Value)
                    .HasColumnType("nvarchar(200)")
                    .HasColumnName(nameof(User.Name));

            builder.OwnsOne(p => p.UserName)
                    .Property(p => p.Value)
                    .HasColumnType("nvarchar(20)")
                    .HasColumnName(nameof(User.UserName));

            builder.OwnsOne(p => p.Password)
                    .Property(p => p.Value)
                    .HasColumnType("nvarchar(20)")
                    .HasColumnName(nameof(User.Password));

            builder.Property(p => p.Email)
                .HasColumnType("nvarchar(20)")
                .HasColumnName(nameof(User.Email));
                
        }
    }
}

