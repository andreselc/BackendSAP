using System.ComponentModel.DataAnnotations;

namespace BackendSAP.Modelos.Dtos.Usuarios
{
    public class UsuarioDatosDto
    {
        [Key]
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Nombre { get; set; }
    }
}
