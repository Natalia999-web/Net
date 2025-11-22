using System;
using System.ComponentModel.DataAnnotations;

namespace AngieGiraldo.Models
{
    public class Productos
    {
        [Key]
        public int IdProducto { get; set; } // Se recomienda tener una clave primaria numérica

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        public decimal precio { get; set; }

        [Required]
        public int Cantidad { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
