﻿using System.ComponentModel.DataAnnotations;

namespace BackendSAP.Modelos.Dtos.Calificaciones
{
    public class CrearCalificacionesDto
    {
        [Required(ErrorMessage = "La observación hacia el psicólogo es obligatoria")]
        [MaxLength(1000, ErrorMessage = "El número máximo de caracteres es de 1000")]
        public string observacion { get; set; }
        public ushort puntaje { get; set; }
        public string usuarioId { get; set; } // Clave Foránea del usuario quién hace la calificación
        public string psicologoId { get; set; } // Clave Foránea del usuario psicólogo quién recibe la calificación
    }
}
