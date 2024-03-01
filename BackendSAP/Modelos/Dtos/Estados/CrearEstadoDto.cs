﻿using System.ComponentModel.DataAnnotations;

namespace BackendSAP.Modelos.Dtos.Estados
{
    public class CrearEstadoDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(60, ErrorMessage = "El número máximo de caracteres es de 60")]
        public string Nombre { get; set; }
    }
}
