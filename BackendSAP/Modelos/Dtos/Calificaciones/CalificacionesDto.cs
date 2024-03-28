using System.ComponentModel.DataAnnotations;

namespace BackendSAP.Modelos.Dtos.Calificaciones
{
    public class CalificacionesDto
    {
        public int Id { get; set; }
        public string observacion { get; set; }
        public int puntaje { get; set; }
        public string usuarioId { get; set; } // Clave Foránea del usuario quién hace la calificación
        public string psicologoId { get; set; } // Clave Foránea del usuario psicólogo quién recibe la calificación
    }
}
