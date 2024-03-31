using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendSAP.Modelos
{
    public class Usuarios : IdentityUser
    {
        //Añadir campos personalizados
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        public int? NumeroColegiatura { get; set; }
        public string? TelefonOficina { get; set; }
        public byte[]? ImagenPerfil { get; set; }
        public byte[]? ImagenTitulo { get; set; }
        public string? DescripcionPsicologo { get; set; }
        public string? Calle_Av { get; set; }
        public string? Verificado { get; set; }
        [MaxLength(1000, ErrorMessage = "El número máximo de caracteres para describir su experiencia es de 1000")]
        public string? Experiencia { get; set; }
        [MaxLength(1000, ErrorMessage = "El número máximo de caracteres para describir su formación es de 1000")]
        public string? Formacion { get; set; }
        [MaxLength(1000, ErrorMessage = "El número máximo de caracteres para describir el tipo de terapia es de 1000")]
        public string? TipoTerapia { get; set; }

        public int? CiudadId { get; set; } // Clave Foránea

        [ForeignKey("CiudadId")] 
        public Ciudades? Ciudades { get; set; }

        public List<EspecialidadPsicologo> EspecialidadPsicologos { get; } = new List<EspecialidadPsicologo>();

        public List<Calificaciones> CalificacionesHechas { get; set; } // Relación con las calificaciones hechas por este usuario

        public List<Calificaciones> CalificacionesRecibidas { get; set; } // Relación con las calificaciones recibidas por este usuario (psicólogo)
    }

}

