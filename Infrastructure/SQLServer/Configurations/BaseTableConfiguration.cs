using System;
using Domain.Common.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.SQLServer.Configurations
{
    public class BaseTableConfiguration : IEntityTypeConfiguration<BaseTable>
    {
        public void Configure(EntityTypeBuilder<BaseTable> builder)
        {
            builder.ToTable(nameof(BaseTable), SQLServerConstants.DefaultSchema);
            builder.HasKey(x => x.Id);
            builder.OwnsOne(p => p.Code)
                    .Property(p => p.Value)
                    .HasColumnName(nameof(BaseTable.Code));

            builder.OwnsOne(p => p.Description)
                    .Property(p => p.Value)
                    .HasColumnName(nameof(BaseTable.Description));


        }
    }
}

