using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infrastructure.Persistences.Contexts.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("ProductId"); // Debe coincidir exacto con tu SQL



            // Configuración de campos básicos
            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired(); // Un producto siempre debe tener nombre

            builder.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false); // Optimización para códigos de barras

            builder.Property(e => e.Image)
                .HasMaxLength(500)      // Espacio suficiente para la URL de Azure
                .IsUnicode(false)       // Las URLs no necesitan caracteres especiales (ahorra espacio)
                .IsRequired(false);     // Permite que el producto no tenga imagen

            builder.Property(e => e.Stock)
                .HasDefaultValue(0); // Para tu demo, inicia en 0 si no se provee

            builder.Property(e => e.SellPrice)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

            
            // Relación con Categoría
            builder.HasOne(d => d.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId) // Asegúrate que tu entidad tenga esta propiedad
                .OnDelete(DeleteBehavior.NoAction);

            // Relación con Proveedor
            builder.HasOne(d => d.Provider)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.ProviderId) // Asegúrate que tu entidad tenga esta propiedad
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
