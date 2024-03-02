using System.ComponentModel.DataAnnotations;

namespace BackendSAP.Modelos.Dtos.Usuarios
{
    public class UsuarioRegistroDto
    {
        [Required(ErrorMessage = "El Usuario es Obligatorio")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "El Nombre es Obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El Password es Obligatorio")]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
