using Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura.Data.Config
{
    public class CompaniaConfiguration : IEntityTypeConfiguration<compania>
    {
        public void Configure(EntityTypeBuilder<compania> builder)
        {

            // Establecemos las propiedades para cuando se creen la tabla de compania
            // en la base de datos con EF

            builder.Property(c => c.Id).IsRequired();
            builder.Property(c => c.Nombre).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Direccion).IsRequired().HasMaxLength(150);
            builder.Property(c => c.Telefono).IsRequired().HasMaxLength(40);
            
        }
    }
}

