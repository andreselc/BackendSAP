using System.ComponentModel.DataAnnotations;

namespace BackendSAP.Modelos.Dtos.Usuarios
{
    public class UsuarioActualizarDto
    {
        public string Id { get; set; }
        public string? Email { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? TelefonOficina { get; set; }
        /*public byte[]? ImagenPerfil { get; set; }*/
        public int? CiudadId { get; set; } 

    }
}
