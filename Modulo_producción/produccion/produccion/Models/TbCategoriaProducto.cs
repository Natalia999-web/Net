using System;
using System.Collections.Generic;

namespace produccion.Models;

public partial class TbCategoriaProducto
{
    public int IdCategoria { get; set; }

    public string NombreCategoria { get; set; } = null!;

    public string? Descripcion { get; set; }

    public byte Estado { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual ICollection<TbProducto> TbProductos { get; set; } = new List<TbProducto>();
}
