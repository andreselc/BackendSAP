using AutoMapper;
using BackendSAP.Modelos;
using BackendSAP.Modelos.Dtos.Estados;
using BackendSAP.Repositorios.IRepositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BackendSAP.Controllers

{
    [ApiController]
    [Route("api/estados")]
    public class EstadosController : ControllerBase
    {
        private readonly IEstadoRepositorio _esRepo;
        private readonly IMapper _mapper;

        public EstadosController(IEstadoRepositorio esRepo, IMapper mapper)
        {
            _esRepo = esRepo;
            _mapper = mapper;   
        }

      [AllowAnonymous]
      [HttpGet]
      //[ResponseCache(Duration = 20)]
      //[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
      [ProducesResponseType(StatusCodes.Status403Forbidden)]
      [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetEstados()
        {
            var listaEstados = _esRepo.GetEstados();
            var listaEstadosDto = new List<EstadoDto>();
            foreach (var lista in listaEstados) 
            {
                listaEstadosDto.Add(_mapper.Map<EstadoDto>(lista));
            }
            return Ok(listaEstadosDto);
        }

        [AllowAnonymous]
        [HttpGet("{estadoId}", Name= "GetEstado")]
        //[ResponseCache(Duration = 30)]
        //[ResponseCache(CacheProfileName = "PorDefecto20Segundos")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetEstado(int estadoId)
        {
            var itemEstado = _esRepo.GetEstado(estadoId);
            if(itemEstado == null)
            {
                return NotFound();
            }

            var itemEstadoDto = _mapper.Map<EstadoDto>(itemEstado);
            return Ok(itemEstadoDto);
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(EstadoDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearEstado([FromBody] CrearEstadoDto crearEstadoDto)
        {
            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState);
            }
            if(crearEstadoDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_esRepo.ExisteEstado(crearEstadoDto.Nombre)) 
            {
                ModelState.AddModelError("", "El estado ya existe");
                return StatusCode(404, ModelState);
            }

            var estado = _mapper.Map<Estados>(crearEstadoDto);
            if (!_esRepo.CrearEstado(estado)) 
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {estado.Nombre}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetEstado", new { estadoId = estado.Id}, estado);
        }

        [Authorize(Roles = "admin")]
        [HttpPatch("{estadoId}", Name = "ActualizarPatchEstado")]
        [ProducesResponseType(201, Type = typeof(EstadoDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ActualizarPatchEstado(int estadoId ,[FromBody] EstadoDto estadoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (estadoDto == null || !_esRepo.ExisteEstado(estadoId))
            {
                return BadRequest(ModelState);
            }
       
            var estado = _mapper.Map<Estados>(estadoDto);
            if (!_esRepo.ActualizarEstado(estado))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro {estado.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{estadoId}", Name = "EliminarEstado")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EliminarEstado(int estadoId)
        {
            if (!_esRepo.ExisteEstado(estadoId))
            {
                return NotFound();
            }

            var estado = _esRepo.GetEstado(estadoId);

            if (!_esRepo.BorrarEstado(estado))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro {estado.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }
}
