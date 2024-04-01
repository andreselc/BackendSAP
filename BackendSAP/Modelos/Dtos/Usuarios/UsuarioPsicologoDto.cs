using BackendSAP.Modelos.Dtos.Calificaciones;
using System.ComponentModel.DataAnnotations;

namespace BackendSAP.Modelos.Dtos.Usuarios

{
    public class UsuarioPsicologoDto
    {
        [Key]
        public string Id { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string TelefonOficina { get; set; }
        public int NumeroColegiatura { get; set; }
        /*public byte[]? ImagenPerfil { get; set; }
        public byte[]? ImagenTitulo { get; set; }*/
        public string DescripcionPsicologo { get; set; }
        public string Calle_Av { get; set; }
        public string Verificado { get; set; }
        public string Experiencia { get; set; }
        public string Formacion { get; set; }
        public string TipoTerapia { get; set; }
        public List<CalificacionesDto> CalificacionesRecibidas { get; set; }

    }
}
