using Azure;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendSAP.Modelos
{
    public class EspecialidadPsicologo //Modelo que representa la intersección entre psicólogo y el trastorno
        //...en el cual se especializa
    {
        [Key]
        public string psicologoId { get; set; }
        [Key]
        public int trastornoId { get; set; }
        [ForeignKey("psicologoId")]
        public Usuarios psicologo { get; set; } = null!;
        [ForeignKey("trastornoId")]
        public TrastornoPsicologico trastorno { get; set; } = null!;
    }
}
