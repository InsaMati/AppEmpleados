using System.ComponentModel.DataAnnotations;

namespace Core.Entidades
{
    public class compania
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la compañia es requerido.")]
        [MaxLength(100, ErrorMessage = "No sea mayor a 100")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La {0} es requerida.")]
        [MaxLength(150, ErrorMessage = "No sea mayor a 150")]
        [Display(Name = "Direccion")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El numero de telefono es requerido.")]
        [MaxLength(40, ErrorMessage = "No sea mayor a 40")]
        
        public int Telefono { get; set; }
         
        [MaxLength(40, ErrorMessage = "No sea mayor a 40")]
        public int Telefono2 { get; set; }
    }
}