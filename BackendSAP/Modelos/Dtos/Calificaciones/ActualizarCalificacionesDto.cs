using System.ComponentModel.DataAnnotations;

namespace BackendSAP.Modelos.Dtos.Calificaciones
{
    public class ActualizarCalificacionesDto
    {
        public int Id { get; set; }
        public string? observacion { get; set; }
        public enum puntaje : ushort { uno = 1, dos = 2, tres = 3, cuatro = 4, cinco = 5 }
    }
}
