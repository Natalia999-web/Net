using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
