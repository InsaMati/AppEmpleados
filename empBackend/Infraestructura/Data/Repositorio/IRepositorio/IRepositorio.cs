using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infraestructura.Data.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T : class
    {
        Task <IEnumerable<T>> ObtenerTodos(
            Expression<Func<T,bool>> filtro = null, // ejemplo: c => c.compania
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, // asc, desc
            string incluirPropiedades = null // "Compania,Cargo" 
        );
        Task<T> ObtenerPrimero(
            Expression<Func<T,bool>> filtro = null, 
            string incluirPropiedades = null);
        Task Agregar(T entidad);            
        void Remover (T entidad);

        // el update, no se pone porque en cada entidad se maneja de manera distinta.
    }
}