using System.ComponentModel.DataAnnotations;

namespace BackendSAP.Modelos.Dtos.Trastornos
{
    public class ActualizarTrastornoDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? Causas { get; set; }
        public string? Sintomas { get; set; }
    }
}

