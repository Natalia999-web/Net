using System;
using System.Collections.Generic;

namespace TechNova.Models
{
    public partial class Venta
    {
        public Venta()
        {
            VentaDetalles = new HashSet<VentaDetalle>();
        }

        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }

        // Propiedad de navegación hacia Cliente
        public virtual Cliente Cliente { get; set; } = null!;

        // Nueva propiedad de navegación hacia VentaDetalle
        public virtual ICollection<VentaDetalle> VentaDetalles { get; set; }
    }
}
