using System.ComponentModel.DataAnnotations;

namespace BackendSAP.Modelos.Dtos.Usuarios
{
    public class UsuarioLoginDto
    {
        [Required(ErrorMessage = "El Usuario es Obligatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El Password es Obligatorio")]
        public string Password { get; set; }

    }
}
