using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entidades
{
    public class empleado
    {
        [Key]        
        public int Id { get; set; }

        public string Apellidos { get; set; }

        public string Nombres { get; set; }

        public string Cargo { get; set; }

        // Referencia para compania
        public int CompaniaId { get; set; }

        // Una relacion entre modelo empleado y modelo compania

        [ForeignKey("CompaniaId")]
        public compania compania { get; set; }
                               
    }
}