using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechNova.Models;

public partial class TechNovaContext : IdentityDbContext
{
    public TechNovaContext() { }

    public TechNovaContext(DbContextOptions<TechNovaContext> options) : base(options) { }

    public virtual DbSet<Cliente> Clientes { get; set; }
    public virtual DbSet<Producto> Productos { get; set; }
    public virtual DbSet<Venta> Ventas { get; set; }
    public virtual DbSet<VentaDetalle> VentaDetalles { get; set; } // ✅

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.NombreCompleto).HasMaxLength(150);
            entity.Property(e => e.Correo).HasMaxLength(150);
            entity.Property(e => e.Direccion).HasMaxLength(250);
            entity.Property(e => e.Telefono).HasMaxLength(50);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Codigo).IsUnique();
            entity.Property(e => e.Codigo).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.Descripcion).HasMaxLength(1000);
            entity.Property(e => e.Precio).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Fecha).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Total).HasColumnType("decimal(18,2)");

            entity.HasOne(d => d.Cliente)
                  .WithMany(c => c.Venta)
                  .HasForeignKey(d => d.ClienteId)
                  .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<VentaDetalle>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Precio).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(18,2)");

            entity.HasOne(d => d.Venta)
                  .WithMany(v => v.VentaDetalles) // ✅ Coincide con la propiedad de Venta
                  .HasForeignKey(d => d.VentaId)
                  .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Producto)
                  .WithMany(p => p.VentaDetalles)
                  .HasForeignKey(d => d.ProductoId)
                  .OnDelete(DeleteBehavior.ClientSetNull);
        });
    }
}
