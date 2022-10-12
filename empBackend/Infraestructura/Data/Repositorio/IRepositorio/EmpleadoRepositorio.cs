using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;

namespace Infraestructura.Data.Repositorio.IRepositorio
{
    public class EmpleadoRepositorio : Repositorio<empleado>, IEmpleadoRepositorio
    {
        private readonly ApplicationDbContext _dbContext;
        public EmpleadoRepositorio(ApplicationDbContext db) : base(db)
        {
            this._dbContext = db;
        }

        public void Actualizar(empleado emp)
        {
            var empleadoDB = _dbContext.Empleado.FirstOrDefault(c => c.Id == emp.Id);

            if(empleadoDB!= null)
            {
                empleadoDB.Nombres = emp.Nombres;
                empleadoDB.Apellidos = emp.Apellidos;
                empleadoDB.Cargo = emp.Cargo;
                empleadoDB.CompaniaId = emp.CompaniaId;
                
                _dbContext.SaveChanges();
            }
        }
    }
}