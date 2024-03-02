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

        public int EstadoId { get; set; } // Clave Foránea

        [ForeignKey("EstadoId")]
        public Estados Estados { get; set; } 

        public ICollection<Usuarios> Usuarios { get; } = new List<Usuarios>(); // Collection navigation containing dependents
    }
}
