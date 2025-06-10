using Microsoft.EntityFrameworkCore;
using PaternDesign.API.Application.Abstractions;
using PaternDesign.API.Domain.Abstractions.Repositories;
using PaternDesign.API.Infraestructure.Data;

namespace PaternDesign.API.Domain.Abstractions.Implementations
{
    public class Repositorio<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repositorio(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> ObtenerTodosAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> ObtenerPorIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AgregarAsync(T entidad)
        {
            await _dbSet.AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(T entidad)
        {
            _dbSet.Update(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await ObtenerPorIdAsync(id);
            _dbSet.Remove(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> ObtenerPorPrecio(ISpecification<T> spec)
        {
            IQueryable<T> query = _dbSet;

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            foreach (var include in spec.Includes)
            {
                query = query.Include(include);
            }

            foreach (var orderBy in spec.OrderBy)
            {
                query = orderBy(query);
            }

            foreach (var orderByDescending in spec.OrderByDescending)
            {
                query = orderByDescending(query);
            }

            if (spec.Skip > 0)
            {
                query = query.Skip(spec.Skip);
            }

            if (spec.Take > 0)
            {
                query = query.Take(spec.Take);
            }

            return await query.ToListAsync();
        }
    }
}
