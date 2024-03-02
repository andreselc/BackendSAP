using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendSAP.Modelos.Dtos.Ciudades
{
    public class CiudadDto
    {

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }
        public int estadoId { get; set; }
    }
}
