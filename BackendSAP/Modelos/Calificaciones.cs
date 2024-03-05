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
        public ushort puntaje { get; set; }
        public string usuarioId { get; set; } // Clave Foránea del usuario quién hace la calificación
        public string psicologoId { get; set; } // Clave Foránea del usuario psicólogo quién recibe la calificación

        [ForeignKey("usuarioId")]
        [InverseProperty("CalificacionesHechas")]
        public Usuarios UsuariosCalificadores { get; set; }

        [ForeignKey("psicologoId")]
        [InverseProperty("CalificacionesRecibidas")]
        public Usuarios UsuariosPsicologos { get; set; }

    }
}
