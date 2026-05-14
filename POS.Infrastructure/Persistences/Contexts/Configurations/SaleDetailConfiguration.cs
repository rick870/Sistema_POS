using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infrastructure.Persistences.Contexts.Configurations
{
    public class SaleDetailConfiguration : IEntityTypeConfiguration<SaleDetail>
    {
        public void Configure(EntityTypeBuilder<SaleDetail> builder)
        {
            builder.HasKey(e => new { e.SaleId, e.ProductId });

            builder.Property(e => e.UnitSalePrice).HasColumnType("decimal(10,2)");
            builder.Property(e => e.Total).HasColumnType("decimal(10,2)");

            // --- AGREGA ESTO AQUÍ ---
            builder.HasOne(x => x.Product)
                .WithMany(x => x.SaleDetails) 
                .HasForeignKey(x => x.ProductId);

            builder.HasOne(x => x.Sale)
                .WithMany(x => x.SaleDetails)
                .HasForeignKey(x => x.SaleId);
            // ------------------------
        }
    }
}