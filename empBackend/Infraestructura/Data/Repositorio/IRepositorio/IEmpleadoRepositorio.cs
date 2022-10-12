using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;

namespace Infraestructura.Data.Repositorio.IRepositorio
{
    public interface IEmpleadoRepositorio : IRepositorio<empleado>
    {
        void Actualizar(empleado emp);
    }
}