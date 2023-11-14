using BlazorServerCrud.Model;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerCrud.Data.AppDbContext
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}
