using System.ComponentModel.DataAnnotations;
using System.Data;
namespace CRUDnativo.Models
{
    public class Productos
    {
        [Key]
        public string Nombre { get; set; }

        public decimal precio { get; set; }

        public int Cantidad { get; set; }

        public DateTime FechaCreacion { get; set; }

       
      


       
    }
}
