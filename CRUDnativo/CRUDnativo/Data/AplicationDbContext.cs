using CRUDnativo.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDnativo.Data
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Productos> Producto { get; set; }
    }
}
