using System.ComponentModel.DataAnnotations;

namespace BackendSAP.Modelos.Dtos.Usuarios
{
    public class UsuarioLoginDto
    {
        [Required(ErrorMessage = "El Usuario es Obligatorio")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El Password es Obligatorio")]
        public string Password { get; set; }

    }
}
