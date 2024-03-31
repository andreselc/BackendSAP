using System.ComponentModel.DataAnnotations;

namespace BackendSAP.Modelos.Dtos.Usuarios
{
    public class UsuarioDto
    {
        [Key]
        public string Id { get; set; }

        public string Email { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string TelefonOficina { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}
