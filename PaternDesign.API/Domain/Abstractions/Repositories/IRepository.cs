using PaternDesign.API.Application.Abstractions;

namespace PaternDesign.API.Domain.Abstractions.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> ObtenerTodosAsync();
        Task<IEnumerable<T>> ObtenerPorPrecio(ISpecification<T> spec);
        Task<T> ObtenerPorIdAsync(int id);
        Task AgregarAsync(T entidad);
        Task ActualizarAsync(T entidad);
        Task EliminarAsync(int id);
    }
}
