using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechNova.Models
{
    public class ProductoSeleccionado
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set; } = "";
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int Cantidad { get; set; } = 1;
        public bool Seleccionado { get; set; } = false;
    }

    public class NuevaVentaViewModel
    {
        [Required]
        public int ClienteId { get; set; }

        public List<ProductoSeleccionado> Productos { get; set; } = new List<ProductoSeleccionado>();
        public decimal Total { get; set; }
    }
}
