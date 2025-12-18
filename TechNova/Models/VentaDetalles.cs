using System.ComponentModel.DataAnnotations.Schema;

namespace TechNova.Models
{
    public class VentaDetalles
    {
        public int Id { get; set; }

        // Relación con la venta
        public int VentaId { get; set; }
        public Venta Venta { get; set; } = null!;

        // Relación con el producto
        public int ProductoId { get; set; }
        public Producto Producto { get; set; } = null!;

        // Cantidad comprada
        public int Cantidad { get; set; }

        // Precio unitario al momento de la venta
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

        // Subtotal calculado automáticamente
        [NotMapped]
        public decimal Subtotal => Cantidad * Precio;
    }
}
