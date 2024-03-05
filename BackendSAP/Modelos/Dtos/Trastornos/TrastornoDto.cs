using System.ComponentModel.DataAnnotations;

namespace BackendSAP.Modelos.Dtos.Trastornos
{
    public class TrastornoDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del trastorno es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La descripción del trastorno es obligatoria")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Las causas del trastorno son obligatorias")]
        public string Causas { get; set; }
        [Required(ErrorMessage = "Los síntomas del trastorno son obligatorias")]
        public string Sintomas { get; set; }
        public DateTime FechaPublicacion { get; set; }

    }
}
