﻿using AutoMapper;
using BackendSAP.Modelos.Dtos.Calificaciones;
using BackendSAP.Modelos;
using BackendSAP.Repositorios.IRepositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendSAP.Controllers
{
    [ApiController]
    [Route("api/calificaciones")]
    public class CalificacionesController: ControllerBase
    {
        private readonly ICalificacionesRepositorio _caliRepo;
        private readonly IMapper _mapper;

        public CalificacionesController(ICalificacionesRepositorio caliRepo, IMapper mapper)
        {
            _caliRepo = caliRepo;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        //[ResponseCache(Duration = 20)]
        //[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCalificaciones()
        {
            var listaCalificacioness = _caliRepo.GetCalificaciones();
            var listaCalificacionessDto = new List<CalificacionesDto>();
            foreach (var lista in listaCalificacioness)
            {
                listaCalificacionessDto.Add(_mapper.Map<CalificacionesDto>(lista));
            }
            return Ok(listaCalificacionessDto);
        }

        [AllowAnonymous]
        [HttpGet("{calificacionId}", Name = "GetCalificaciones")]
        //[ResponseCache(Duration = 30)]
        //[ResponseCache(CacheProfileName = "PorDefecto20Segundos")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCalificacion(int calificacionId)
        {
            var itemCalificaciones = _caliRepo.GetCalificacion(calificacionId);
            if (itemCalificaciones == null)
            {
                return NotFound();
            }

            var itemCalificacionesDto = _mapper.Map<CalificacionesDto>(itemCalificaciones);
            return Ok(itemCalificacionesDto);
        }

        [Authorize(Roles = "admin")]
        [Authorize(Roles = "psicologo")]
        [Authorize(Roles = "usuario")]
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CalificacionesDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearCalificaciones([FromBody] CrearCalificacionesDto crearCalificacionesDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (crearCalificacionesDto == null)
            {
                return BadRequest(ModelState);
            }

            var calificaciones = _mapper.Map<Calificaciones>(crearCalificacionesDto);
            if (!_caliRepo.CrearCalificacion(calificaciones))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {calificaciones.Id}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetCalificaciones", new { CalificacionesId = calificaciones.Id }, calificaciones);
        }

        [Authorize(Roles = "admin")]
        [Authorize(Roles = "psicologo")]
        [Authorize(Roles = "usuario")]
        [HttpPatch("{calificacionesId}", Name = "ActualizarPatchCalificaciones")]
        [ProducesResponseType(201, Type = typeof(CalificacionesDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ActualizarPatchCalificaciones(int calificacionesId, [FromBody] ActualizarCalificacionesDto calificacionesDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (calificacionesDto == null || !_caliRepo.ExisteCalificacion(calificacionesId))
            {
                return BadRequest(ModelState);
            }

            var calificaciones = _mapper.Map<Calificaciones>(calificacionesDto);
            if (!_caliRepo.ActualizarCalificacion(calificaciones))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro {calificaciones.Id}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{calificacionesId}", Name = "EliminarCalificaciones")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EliminarCalificaciones(int calificacionesId)
        {
            if (!_caliRepo.ExisteCalificacion(calificacionesId))
            {
                return NotFound();
            }

            var calificaciones = _caliRepo.GetCalificacion(calificacionesId);

            if (!_caliRepo.BorrarCalificacion(calificaciones))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro {calificaciones.Id}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }
}
