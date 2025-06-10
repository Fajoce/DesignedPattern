namespace PaternDesign.API.Application.Utils
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; } // Los elementos de la página actual
        public int TotalItems { get; set; } // Total de elementos sin paginar
        public int PageNumber { get; set; } // Número de la página actual
        public int PageSize { get; set; } // Tamaño de la página (cuántos elementos por página)
        public int TotalPages { get; set; } // Total de páginas
    }
}
