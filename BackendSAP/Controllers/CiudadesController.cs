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
        private readonly ICiudadRepositorio _ciRepo;
        private readonly IMapper _mapper;

        public CiudadesController(ICiudadRepositorio ciRepo, IMapper mapper)
        {
            _ciRepo = ciRepo;
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
            var listaCiudades = _ciRepo.GetCiudades();
            var listaCiudadesDto = new List<CiudadDto>();
            foreach (var lista in listaCiudades) 
            {
                listaCiudadesDto.Add(_mapper.Map<CiudadDto>(lista));
            }
            return Ok(listaCiudadesDto);
        }

        [AllowAnonymous]
        [HttpGet("{CiudadId}", Name= "GetCiudad")]
        //[ResponseCache(Duration = 30)]
        //[ResponseCache(CacheProfileName = "PorDefecto20Segundos")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCiudad(int CiudadId)
        {
            var itemCiudad = _ciRepo.GetCiudad(CiudadId);
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
            if(crearCiudadDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_ciRepo.ExisteCiudad(crearCiudadDto.Nombre)) 
            {
                ModelState.AddModelError("", "La Categoría ya existe");
                return StatusCode(404, ModelState);
            }

            var Ciudad = _mapper.Map<Ciudades>(crearCiudadDto);
            if (!_ciRepo.CrearCiudad(Ciudad)) 
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {Ciudad.Nombre}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetCiudad", new { CiudadId = Ciudad.Id}, Ciudad);
        }

        //[Authorize(Roles = "admin")]
        [HttpPatch("{CiudadId}", Name = "ActualizarPatchCiudad")]
        [ProducesResponseType(201, Type = typeof(CiudadDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ActualizarPatchCiudad(int CiudadId ,[FromBody] CiudadDto CiudadDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (CiudadDto == null || !_ciRepo.ExisteCiudad(CiudadId))
            {
                return BadRequest(ModelState);
            }
       
            var Ciudad = _mapper.Map<Ciudades>(CiudadDto);
            if (!_ciRepo.ActualizarCiudad(Ciudad))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro {Ciudad.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        //[Authorize(Roles = "admin")]
        [HttpDelete("{CiudadId}", Name = "EliminarCiudad")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EliminarCiudad(int CiudadId)
        {
            if (!_ciRepo.ExisteCiudad(CiudadId))
            {
                return NotFound();
            }

            var Ciudad = _ciRepo.GetCiudad(CiudadId);

            if (!_ciRepo.BorrarCiudad(Ciudad))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro {Ciudad.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }
}
