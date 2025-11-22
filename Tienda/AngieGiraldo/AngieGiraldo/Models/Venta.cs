using AngieGiraldo.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngieGiraldo.Models
{
    public class Venta
    {
        [Key]
        public int IdVenta { get; set; }

        [Required]
        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }

        [Required]
        [ForeignKey("Producto")]
        public int IdProducto { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public decimal Total { get; set; }

        public DateTime FechaVenta { get; set; } = DateTime.Now;

        // Relaciones (navegación)
        public virtual Cliente Cliente { get; set; }
        public virtual Productos Producto { get; set; }
    }
}
