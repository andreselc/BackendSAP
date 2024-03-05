using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendSAP.Modelos
{
    public class Calificaciones
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string observacion { get; set; }
        public int usuarioId { get; set; } // Clave Foránea del usuario quién hace la calificación
        public int psicologoId { get; set; } // Clave Foránea del usuario psicólogo quién recibe la calificación

        [ForeignKey("usuarioId")]
        public Usuarios Usuarios { get; set; }

        [ForeignKey("psicologoId")]
        public Usuarios UsuariosPsicologos { get; set; }

    }
}
