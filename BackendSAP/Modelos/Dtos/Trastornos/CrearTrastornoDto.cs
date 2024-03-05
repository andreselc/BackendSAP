using System.ComponentModel.DataAnnotations;

namespace BackendSAP.Modelos.Dtos.Trastornos
{
    public class CrearTrastornoDto
    {
        [Required(ErrorMessage = "El Nombre del trastorno es obligatorio")]
        [MaxLength(100, ErrorMessage = "El número máximo de caracteres es de 100")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La descripción del trastorno es obligatoria")]
        [MaxLength(1000, ErrorMessage = "El número máximo de caracteres es de 1000")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Las causas del trastorno son obligatorias")]
        [MaxLength(1000, ErrorMessage = "El número máximo de caracteres es de 1000")]
        public string Causas { get; set; }
        [Required(ErrorMessage = "Los síntomas del trastorno son obligatorias")]
        [MaxLength(1000, ErrorMessage = "El número máximo de caracteres es de 1000")]
        public string Sintomas { get; set; }
        public DateTime FechaPublicacion { get; set; }
    }
}
