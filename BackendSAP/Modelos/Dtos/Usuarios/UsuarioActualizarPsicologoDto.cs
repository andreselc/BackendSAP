using System.ComponentModel.DataAnnotations;

namespace BackendSAP.Modelos.Dtos.Usuarios
{
    public class UsuarioActualizarPsicologoDto
    {
        public string Id { get; set; }
        public string? Email { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public int? NumeroColegiatura { get; set; }
        public string? TelefonOficina { get; set; }
        /*public byte[]? ImagenPerfil { get; set; }
        public byte[]? ImagenTitulo { get; set; }*/
        public string? DescripcionPsicologo { get; set; }
        public string? Calle_Av { get; set; }
        public string? Verificado { get; set; }
        [MaxLength(1000, ErrorMessage = "El número máximo de caracteres para describir su experiencia es de 1000")]
        public string? Experiencia { get; set; }
        [MaxLength(1000, ErrorMessage = "El número máximo de caracteres para describir su formación es de 1000")]
        public string? Formacion { get; set; }
        [MaxLength(1000, ErrorMessage = "El número máximo de caracteres para describir el tipo de terapia es de 1000")]
        public string? TipoTerapia { get; set; }
        public int? CiudadId { get; set; } 

    }
}
