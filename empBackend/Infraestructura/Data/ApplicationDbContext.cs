using Core.Entidades;
using Infraestructura.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          modelBuilder.ApplyConfiguration(new CompaniaConfiguration());
          modelBuilder.ApplyConfiguration(new EmpleadoConfiguration());
        }

        public DbSet<compania> Compania { get; set; }
        public DbSet<empleado> Empleado { get; set; }
    }
}