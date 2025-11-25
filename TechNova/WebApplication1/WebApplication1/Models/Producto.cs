using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal PrecioUnitario { get; set; }

    public int Stock { get; set; }

    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();
}
