using System.ComponentModel.DataAnnotations;

namespace BackendSAP.Modelos.Dtos.Calificaciones
{
    public class ActualizarCalificacionesDto
    {
        public int Id { get; set; }
        public string? observacion { get; set; }
        public ushort? puntaje { get; set; }
    }
}
