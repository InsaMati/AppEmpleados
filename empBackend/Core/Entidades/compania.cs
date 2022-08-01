using System.ComponentModel.DataAnnotations;

namespace Core.Entidades
{
    public class compania
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Direccion { get; set; }

        public int Telefono { get; set; }
         
        public int Telefono2 { get; set; }
    }
}