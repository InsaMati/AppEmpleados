using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infraestructura.Data.Repositorio.IRepositorio
{
    public interface IUnitOfWork : IDisposable
    {
        ICompaniaRepositorio compania {get;}
        IEmpleadoRepositorio empleado {get;}     
        Task Guardar();
    }
}