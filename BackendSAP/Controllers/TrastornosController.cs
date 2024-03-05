using AutoMapper;
using BackendSAP.Modelos;
using BackendSAP.Modelos.Dtos.Trastornos;
using BackendSAP.Repositorios.IRepositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendSAP.Controllers
{
    [ApiController]
    [Route("api/trastornos")]
    public class TrastornosController : ControllerBase
    {
        private readonly ITrastornoRepositorio _trasRepo;
        private readonly IMapper _mapper;

        public TrastornosController(ITrastornoRepositorio trasRepo, IMapper mapper)
        {
            _trasRepo = trasRepo;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        //[ResponseCache(Duration = 20)]
        //[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetTrastornos()
        {
            var listaTrastornos = _trasRepo.GetTrastornos();
            var listaTrastornosDto = new List<TrastornoDto>();
            foreach (var lista in listaTrastornos)
            {
                listaTrastornosDto.Add(_mapper.Map<TrastornoDto>(lista));
            }
            return Ok(listaTrastornosDto);
        }

        [AllowAnonymous]
        [HttpGet("{TrastornoId}", Name = "GetTrastorno")]
        //[ResponseCache(Duration = 30)]
        //[ResponseCache(CacheProfileName = "PorDefecto20Segundos")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTrastorno(int TrastornoId)
        {
            var itemTrastorno = _trasRepo.GetTrastorno(TrastornoId);
            if (itemTrastorno == null)
            {
                return NotFound();
            }

            var itemTrastornoDto = _mapper.Map<TrastornoDto>(itemTrastorno);
            return Ok(itemTrastornoDto);
        }

        //[AllowAnonymous]
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(TrastornoDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearTrastorno([FromBody] CrearTrastornoDto crearTrastornoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (crearTrastornoDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_trasRepo.ExisteTrastorno(crearTrastornoDto.Nombre))
            {
                ModelState.AddModelError("", "La Categoría ya existe");
                return StatusCode(404, ModelState);
            }

            var trastorno = _mapper.Map<TrastornoPsicologico>(crearTrastornoDto);
            if (!_trasRepo.CrearTrastorno(trastorno))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {trastorno.Nombre}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetTrastorno", new { TrastornoId = trastorno.Id }, trastorno);
        }

        [Authorize(Roles = "admin")]
        [HttpPatch("{TrastornoId}", Name = "ActualizarPatchTrastorno")]
        [ProducesResponseType(201, Type = typeof(TrastornoDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ActualizarPatchTrastorno(int trastornoId, [FromBody] TrastornoDto trastornoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (trastornoDto == null || !_trasRepo.ExisteTrastorno(trastornoId))
            {
                return BadRequest(ModelState);
            }

            var trastorno = _mapper.Map<TrastornoPsicologico>(trastornoDto);
            if (!_trasRepo.ActualizarTrastorno(trastorno))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro {trastorno.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{trastornoId}", Name = "EliminarTrastorno")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EliminarTrastorno(int trastornoId)
        {
            if (!_trasRepo.ExisteTrastorno(trastornoId))
            {
                return NotFound();
            }

            var trastorno = _trasRepo.GetTrastorno(trastornoId);

            if (!_trasRepo.BorrarTrastorno(trastorno))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro {trastorno.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("Buscar")]
        public IActionResult Buscar(string nombre)
        {
            try
            {
                var resultado = _trasRepo.BuscarTrastorno(nombre.Trim());

                if (resultado.Any())
                {
                    return Ok(resultado);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error recuperando datos");
            }

        }
    }
}
