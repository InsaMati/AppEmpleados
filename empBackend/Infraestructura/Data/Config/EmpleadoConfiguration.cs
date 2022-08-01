using Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura.Data.Config
{
    public class EmpleadoConfiguration : IEntityTypeConfiguration<empleado>
    {
        public void Configure(EntityTypeBuilder<empleado> builder)
        {
            builder.Property(e => e.Id).IsRequired();
            builder.Property(e => e.Apellidos).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Nombres).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Cargo).IsRequired().HasMaxLength(100);
            builder.Property(e => e.CompaniaId).IsRequired();

            // Todo Relaciones

            // Relacion uno a muchos
         
            builder.HasOne(e => e.compania).WithMany()
                   .HasForeignKey(e => e.CompaniaId);

        }
    }
}