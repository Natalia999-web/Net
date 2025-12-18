using System;
using System.Collections.Generic;

namespace TechNova.Models
{
    public partial class Producto
    {
        public Producto()
        {
            VentaDetalles = new HashSet<VentaDetalle>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        // Nueva propiedad de navegación hacia VentaDetalle
        public virtual ICollection<VentaDetalle> VentaDetalles { get; set; }
    }
}
