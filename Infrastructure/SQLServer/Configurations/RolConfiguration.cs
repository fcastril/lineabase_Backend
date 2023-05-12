using Domain.Common.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.SQLServer.Configurations
{
    public class RolConfiguration : BaseConfiguration<Rol>,IEntityTypeConfiguration<Rol>
    {
        public override void Configure(EntityTypeBuilder<Rol> builder)
        {
            base.Configure(builder);

            builder.OwnsOne(p => p.Name)
                    .Property(p => p.Value)
                    .HasColumnType("nvarchar(20)")
                    .HasColumnName(nameof(Rol.Name));

            builder.OwnsOne(p => p.Description)
                    .Property(p => p.Value)
                    .HasColumnType("nvarchar(200)")
                    .HasColumnName(nameof(Rol.Description));

            builder.Property(x => x.Root)
                .HasColumnType("bit")
                .HasDefaultValue(0)
                .IsRequired(true);

        }
    }
}

