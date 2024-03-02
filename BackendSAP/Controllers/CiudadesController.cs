using AutoMapper;
using BackendSAP.Modelos;
using BackendSAP.Modelos.Dtos.Ciudades;
using BackendSAP.Repositorios.IRepositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BackendSAP.Controllers

{
    [ApiController]
    [Route("api/ciudades")]
    public class CiudadesController : ControllerBase
    {
        private readonly ICiudadRepositorio _ciuRepo;
        private readonly IMapper _mapper;

        public CiudadesController(ICiudadRepositorio ciuRepo, IMapper mapper)
        {
            _ciuRepo = ciuRepo;
            _mapper = mapper;   
        }

      [AllowAnonymous]
      [HttpGet]
      //[ResponseCache(Duration = 20)]
      //[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
      [ProducesResponseType(StatusCodes.Status403Forbidden)]
      [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCiudades()
        {
            var listaCiudades = _ciuRepo.GetCiudades();
            var listaCiudadesDto = new List<CiudadDto>();
            foreach (var lista in listaCiudades) 
            {
                listaCiudadesDto.Add(_mapper.Map<CiudadDto>(lista));
            }
            return Ok(listaCiudadesDto);
        }

        [AllowAnonymous]
        [HttpGet("{ciudadId}", Name= "GetCiudad")]
        //[ResponseCache(Duration = 30)]
        //[ResponseCache(CacheProfileName = "PorDefecto20Segundos")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCiudad(int ciudadId)
        {
            var itemCiudad = _ciuRepo.GetCiudad(ciudadId);
            if(itemCiudad == null)
            {
                return NotFound();
            }

            var itemCiudadDto = _mapper.Map<CiudadDto>(itemCiudad);
            return Ok(itemCiudadDto);
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CiudadDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearCiudad([FromBody] CiudadDto crearCiudadDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (crearCiudadDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_ciuRepo.ExisteCiudad(crearCiudadDto.Nombre))
            {
                ModelState.AddModelError("", "La Ciudad ya existe");
                return StatusCode(404, ModelState);
            }

            var ciudad = _mapper.Map<Ciudades>(crearCiudadDto);
            if (!_ciuRepo.CrearCiudad(ciudad))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {ciudad.Nombre}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetCiudad", new { ciudadId = ciudad.Id }, ciudad);
        }

        //[Authorize(Roles = "admin")]
        [HttpPatch("{ciudadId}", Name = "ActualizarPatchCiudad")]
        [ProducesResponseType(201, Type = typeof(CiudadDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ActualizarPatchCiudad(int ciudadId ,[FromBody] CiudadDto ciudadDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ciudadDto == null || !_ciuRepo.ExisteCiudad(ciudadId))
            {
                return BadRequest(ModelState);
            }
       
            var ciudad = _mapper.Map<Ciudades>(ciudadDto);
            if (!_ciuRepo.ActualizarCiudad(ciudad))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro {ciudad.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        //[Authorize(Roles = "admin")]
        [HttpDelete("{ciudadId}", Name = "EliminarCiudad")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EliminarCiudad(int ciudadId)
        {
            if (!_ciuRepo.ExisteCiudad(ciudadId))
            {
                return NotFound();
            }

            var ciudad = _ciuRepo.GetCiudad(ciudadId);

            if (!_ciuRepo.BorrarCiudad(ciudad))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro {ciudad.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("GetCiudadEnEstado/{estadoId}")]
        public IActionResult GetCiudadEnEstado(int estadoId)
        {
            var listaCiudades = _ciuRepo.GetCiudadesEnEstado(estadoId);

            if (listaCiudades == null)
            {
                return NotFound();
            }

            var itemCiudad = new List<CiudadDto>();

            foreach (var item in listaCiudades)
            {
                itemCiudad.Add(_mapper.Map<CiudadDto>(item));
            }

            return Ok(itemCiudad);
        }

        [AllowAnonymous]
        [HttpGet("Buscar")]
        public IActionResult Buscar(string nombre)
        {
            try
            {
                var resultado = _ciuRepo.BuscarCiudad(nombre.Trim());

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
