using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendSAP.Modelos
{
    public class Ciudades
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }

        public int EstadoId { get; set; } // Cambio en el nombre de la propiedad

        [ForeignKey("EstadoId")] // Corrección en el nombre de la FK
        public Estados Estados { get; set; } // Cambio en el nombre de la propiedad de navegación
    }
}
