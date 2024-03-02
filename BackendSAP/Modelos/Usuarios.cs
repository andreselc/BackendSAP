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
        [Required]
        public int NumeroColegiatura { get; set; }
        public int TelefonOficina { get; set; }
        public byte[] ImagenPerfil { get; set; }
        public byte[] ImagenTitulo { get; set; }
        public string DescripcionPsicologo { get; set; }
        public string Calle_Av { get; set; }
        public enum Verificado { V, F }
        public string Experiencia { get; set; }
        public string Formacion { get; set; }
        public string TipoTerapia { get; set; }

        public int CiudadId { get; set; } // Clave Foránea

        [ForeignKey("CiudadId")] 
        public Ciudades Ciudades { get; set; } 

    }

}

