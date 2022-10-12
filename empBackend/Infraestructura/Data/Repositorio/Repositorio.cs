using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infraestructura.Data.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infraestructura.Data.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public DbSet<T> dbSet;
        public Repositorio(ApplicationDbContext db)
        {
            this._context = db;
            this.dbSet = _context.Set<T>();
        }

        public async Task<T> ObtenerPrimero(Expression<Func<T, bool>> filtro = null, string incluirPropiedades = null)
        {
            IQueryable<T> query = dbSet;

            if(filtro!=null)
            {
                query = query.Where(filtro);
            }

            if(incluirPropiedades!=null) // "Compania,Cargo,Departamento, etc etc"
            {
                foreach (var ip in incluirPropiedades.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(ip);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> ObtenerTodos(Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string incluirPropiedades = null)
        {
            IQueryable<T> query = dbSet;

            if(filtro!=null)
            {
                query = query.Where(filtro);
            }

            if(incluirPropiedades!=null) // "Compania,Cargo,Departamento, etc etc"
            {
                foreach (var ip in incluirPropiedades.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(ip);
                }
            }

            if(orderBy!=null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task Agregar(T entidad)
        {
            await dbSet.AddAsync(entidad);
        }

        public void Remover(T entidad)
        {
            dbSet.Remove(entidad);
        }
    }
}