using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public partial class TechNovaContext : IdentityDbContext
{
    public TechNovaContext(DbContextOptions<TechNovaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }
    public virtual DbSet<DetalleVentum> DetalleVenta { get; set; }
    public virtual DbSet<Producto> Productos { get; set; }
    public virtual DbSet<Venta> Ventas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ⚠⚠⚠ ESTO ES OBLIGATORIO PARA QUE IDENTITY FUNCIONE
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente);

            entity.Property(e => e.Correo).HasMaxLength(120);
            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.NombreCompleto).HasMaxLength(120);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<DetalleVentum>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta);

            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Subtotal)
                .HasComputedColumnSql("([Cantidad]*[PrecioUnitario])", true)
                .HasColumnType("decimal(29, 2)");

            entity.HasOne(d => d.IdProductoNavigation)
                .WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdProducto);

            entity.HasOne(d => d.IdVentaNavigation)
                .WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVenta);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto);

            entity.HasIndex(e => e.Codigo).IsUnique();

            entity.Property(e => e.Codigo).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(150);
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta);

            entity.Property(e => e.FechaVenta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdClienteNavigation)
                .WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdCliente);

        });

    }
}
