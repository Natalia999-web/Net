using System;
using System.ComponentModel.DataAnnotations;

namespace AngieGiraldo.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(100)]
        public string Apellido { get; set; }

        [StringLength(200)]
        public string Direccion { get; set; }

        [StringLength(15)]
        public string Telefono { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string Correo { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
