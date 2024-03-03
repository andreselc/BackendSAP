using System.ComponentModel.DataAnnotations;

namespace BackendSAP.Modelos
{
    public class TrastornoPsicologico
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public string Causas { get; set; }
        [Required]
        public string Sintomas { get; set; }
        [Required]
        public DateTime FechaPublicacion { get; set; }
        public List<EspecialidadPsicologo> EspecialidadPsicologos { get; } = [];

    }
}
