using System;
using System.Collections.Generic;

namespace TechNova.Models
{
    public partial class VentaDetalle
    {
        // Identificador único de cada detalle de venta
        public int Id { get; set; }

        // Relación con la venta
        public int VentaId { get; set; }

        // Relación con el producto
        public int ProductoId { get; set; }

        // Cantidad del producto comprado
        public int Cantidad { get; set; }

        // Precio unitario del producto al momento de la venta
        public decimal Precio { get; set; }

        // Subtotal = Cantidad * PrecioUnitario
        public decimal Subtotal { get; set; }

        // Navegación hacia la entidad Producto
        public virtual Producto Producto { get; set; } = null!;

        // Navegación hacia la entidad Venta
        public virtual Venta Venta { get; set; } = null!;
    }
}