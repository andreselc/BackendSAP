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

        [ForeignKey("estadoId")]
        public int estadoId { get; set; } // Required foreign key property
        public Estados Estados { get; set; } = null!; // Required reference navigation to principal
    }
}
