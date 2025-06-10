using Microsoft.EntityFrameworkCore;
using PaternDesign.API.Domain.Entities;
using System.Net.Http.Headers;

namespace PaternDesign.API.Infraestructure.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        public DbSet<Products> Products { get; set; }
    }
}
