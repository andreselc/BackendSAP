using AutoMapper;
using BackendSAP.Modelos.Dtos.Calificaciones;
using BackendSAP.Modelos;
using BackendSAP.Repositorios.IRepositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackendSAP.Controllers
{
    [ApiController]
    [Route("api/calificaciones")]
    public class CalificacionesController: ControllerBase
    {
        private readonly ICalificacionesRepositorio _caliRepo;
        private readonly IMapper _mapper;
        private readonly IUsuarioRepositorio _usRepo;

        public CalificacionesController(ICalificacionesRepositorio caliRepo,IMapper mapper, IUsuarioRepositorio usRepo)
        {
            _caliRepo = caliRepo;
            _mapper = mapper;
            _usRepo = usRepo;
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
        [HttpGet("{calificacionId}", Name = "GetCalificacion")]
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

        [Authorize(Roles = "admin,psicologo,usuario")]
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

            var calificacion = _mapper.Map<Calificaciones>(crearCalificacionesDto);

            if (!_caliRepo.PuntajeEsCorrecto(calificacion.puntaje))
            {
                ModelState.AddModelError("", "Puntaje mal colocado, ingrese un número entero del 1 al 5.");
                return StatusCode(400, ModelState);
            }

            if (_caliRepo.ExisteCalificacionDuplicada(calificacion.usuarioId, calificacion.psicologoId))
            {
                ModelState.AddModelError("", "No puedes registrar dos calificaciones al mismo psicólogo.");
                return StatusCode(400, ModelState);
            }

            if (!_caliRepo.CrearCalificacion(calificacion))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {calificacion.Id}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetCalificacion", new { calificacionId = calificacion.Id }, calificacion);
        }

        [Authorize(Roles = "admin,psicologo,usuario")]
        [HttpPatch("{calificacionId}", Name = "ActualizarPatchCalificaciones")]
        [ProducesResponseType(201, Type = typeof(CalificacionesDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult ActualizarPatchCalificaciones(int calificacionId, [FromBody] ActualizarCalificacionesDto calificacionesDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (calificacionesDto == null || !_caliRepo.ExisteCalificacion(calificacionId))
            {
                return BadRequest(ModelState);
            }

            var calificacion = _mapper.Map<Calificaciones>(calificacionesDto);
            calificacion.usuarioId = _caliRepo.GetCalificacion(calificacionId).usuarioId;
            Usuarios user = _usRepo.GetCurrentUser();

            if (!_caliRepo.PuntajeEsCorrecto(calificacion.puntaje))
            {
                ModelState.AddModelError("", "Puntaje mal colocado, ingrese un número entero del 1 al 5.");
                return StatusCode(400, ModelState);
            }

            if (calificacion.usuarioId != user.Id && !User.IsInRole("admin"))
            {
                ModelState.AddModelError("", $"No es posible editar una calificación que no le pertenece");
                return StatusCode(403, ModelState);
            }

            if (!_caliRepo.ActualizarCalificacion(calificacion))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro {calificacion.Id}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [Authorize(Roles = "admin,psicologo,usuario")]
        [HttpDelete("{calificacionId}", Name = "EliminarCalificaciones")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EliminarCalificaciones(int calificacionId)
        {
            if (!_caliRepo.ExisteCalificacion(calificacionId))
            {
                return NotFound();
            }

            var calificacion = _caliRepo.GetCalificacion(calificacionId);
            Usuarios user = _usRepo.GetCurrentUser();


            if (calificacion.usuarioId != user.Id  && !User.IsInRole("admin"))
            {
                ModelState.AddModelError("", $"No es posible eliminar una calificación que no le pertenece");
                return StatusCode(403, ModelState);
            }

            if (!_caliRepo.BorrarCalificacion(calificacion))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro {calificacion.Id}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }
}
