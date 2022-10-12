using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;
using Infraestructura.Data.Repositorio.IRepositorio;

namespace Infraestructura.Data.Repositorio
{
    public class CompaniaRepositorio : Repositorio<compania>, ICompaniaRepositorio
    {
        private readonly ApplicationDbContext _dbContext;
        public CompaniaRepositorio(ApplicationDbContext db) : base(db)
        {
           this._dbContext = db;
        }
        public void Actualizar(compania comp)
        {
            var companiaDb = _dbContext.Compania.FirstOrDefault(c => c.Id == comp.Id);

            if(companiaDb!= null)
            {
                companiaDb.Nombre = comp.Nombre;
                companiaDb.Direccion = comp.Direccion;
                companiaDb.Telefono = comp.Telefono;
                companiaDb.Telefono2 = comp.Telefono2;

                _dbContext.SaveChanges();
            }
        }
    }
}