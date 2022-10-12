using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infraestructura.Data.Repositorio.IRepositorio;

namespace Infraestructura.Data.Repositorio
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public ICompaniaRepositorio compania {get; private set;}
        public IEmpleadoRepositorio empleado {get; private set;}
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            compania = new CompaniaRepositorio(db);
            empleado = new EmpleadoRepositorio(db);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
        }
    }
}