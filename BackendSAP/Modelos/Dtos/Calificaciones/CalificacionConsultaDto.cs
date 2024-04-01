using System.ComponentModel.DataAnnotations;

namespace BackendSAP.Modelos.Dtos.Calificaciones
{
    public class CalificacionConsultaDto
    {
        public int Id { get; set; }
        public string observacion { get; set; }
        public int puntaje { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}
