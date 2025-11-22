using AngieGiraldo.Models;
using Microsoft.EntityFrameworkCore;

namespace AngieGiraldo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Tablas (DbSet)
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Venta> Ventas { get; set; }

        // Configuración de relaciones entre las tablas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación: Cliente -> Ventas (1 a muchos)
            modelBuilder.Entity<Venta>()
                .HasOne(v => v.Cliente)
                .WithMany() // Si luego agregas una lista de Ventas en Cliente, aquí pondrías ".WithMany(c => c.Ventas)"
                .HasForeignKey(v => v.IdCliente)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación: Producto -> Ventas (1 a muchos)
            modelBuilder.Entity<Venta>()
                .HasOne(v => v.Producto)
                .WithMany() // Igual que arriba, podrías usar .WithMany(p => p.Ventas)
                .HasForeignKey(v => v.IdProducto)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
