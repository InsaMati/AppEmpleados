using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;

namespace Infraestructura.Data.Repositorio.IRepositorio
{
    public interface ICompaniaRepositorio : IRepositorio<compania>
    {
        // Void porque actualizar no es asincrono
        void Actualizar(compania comp);
    }
}