using System.ComponentModel.DataAnnotations;

namespace BackendSAP.Modelos.Dtos.Usuarios
{
    public class UsuarioRegistroDto
    {
        [Required(ErrorMessage = "El Correo Electrónico es Obligatorio")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El Nombre es Obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El Apellido es Obligatorio")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El Password es Obligatorio")]
        public string Password { get; set; }

        public string Role { get; set; }

        public int ciudadId { get; set; }
    }
}
