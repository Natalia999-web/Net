using System;
using System.Collections.Generic;

namespace produccion.Models;

public partial class TbProducto
{
    public int IdProducto { get; set; }

    public string NombreProducto { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public int Stock { get; set; }

    public byte Estado { get; set; }

    public int IdCategoria { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual TbCategoriaProducto? IdCategoriaNavigation { get; set; }
}
