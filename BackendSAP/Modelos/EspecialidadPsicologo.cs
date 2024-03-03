using Azure;
using Microsoft.Extensions.Hosting;

namespace BackendSAP.Modelos
{
    public class EspecialidadPsicologo //Modelo que representa la intersección entre psicólogo y el trastorno
        //...en el cual se especializa
    {
        public int psicologoId { get; set; }
        public int trastornoId { get; set; }
        public Usuarios psicologo { get; set; } = null!;
        public TrastornoPsicologico trastorno { get; set; } = null!;
    }
}
