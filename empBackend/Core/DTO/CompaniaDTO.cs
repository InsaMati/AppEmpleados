using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class CompaniaDTO
    {
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
        public int Telefono { get; set; }
        
        public int Telefono2 { get; set; }
    }
}