using AutoMapper;
using BackendSAP.Modelos;
using BackendSAP.Modelos.Dtos.Usuarios;
using BackendSAP.Repositorios.IRepositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BackendSAP.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usRepo;
        protected RespuestasAPI _respuestasApi;
        private readonly IMapper _mapper;

        public UsuariosController(IUsuarioRepositorio usRepo, IMapper mapper)
        {
            _usRepo = usRepo;
            this._respuestasApi = new();
            _mapper = mapper;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsuariosAsync()
        {
            var usuariosConRoles = await _usRepo.GetUsuarios();
            return Ok(usuariosConRoles);

        }

        [Authorize(Roles = "admin")]
        [HttpGet("{usuarioId}", Name = "GetUsuario")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUsuario(string usuarioId)
        {
            var itemUsuario = _usRepo.GetUsuario(usuarioId);
            if (itemUsuario == null)
            {
                return NotFound();
            }

            var itemUsuarioDto = _mapper.Map<UsuarioDto>(itemUsuario);
            return Ok(itemUsuarioDto);
        }

        [AllowAnonymous]
        [HttpGet("GetUsuarioPsicologo")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUsuarioPsicologo(string nombre)
        {
            try
            {
                var itemUsuario = _usRepo.BuscarUsuarioPorNombre(nombre.Trim());
                if (itemUsuario.Any())
                {
                    var itemUsuarioDto = _mapper.Map<ICollection<UsuarioPsicologoDto>>(itemUsuario);
                    return Ok(itemUsuarioDto);
                }
                return NotFound();
            }

            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error recuperando datos");
            }
        }

        [AllowAnonymous]
        [HttpPost("registro")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Registro([FromBody] UsuarioRegistroDto usuarioRegistroDto)
        {
            bool validarNombreUsuarioUnico = _usRepo.IsUniqueUser(usuarioRegistroDto.Email);
            if (!validarNombreUsuarioUnico)
            {
                _respuestasApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestasApi.IsSuccess = false;
                _respuestasApi.ErrorMessages.Add("El nombre de usuario ya existe");
                return BadRequest(_respuestasApi);
            }

            var usuario = await _usRepo.Registro(usuarioRegistroDto);
            if (usuario == null)
            {
                _respuestasApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestasApi.IsSuccess = false;
                _respuestasApi.ErrorMessages.Add("Error en el registro");
                return BadRequest(_respuestasApi);
            }

            _respuestasApi.StatusCode = HttpStatusCode.OK;
            _respuestasApi.IsSuccess = true;
            return Ok(_respuestasApi);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto usuarioLoginDto)
        {
            var respuestaLogin = await _usRepo.Login(usuarioLoginDto);

            if (respuestaLogin.Usuario == null || string.IsNullOrEmpty(respuestaLogin.Token))
            {
                _respuestasApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestasApi.IsSuccess = false;
                _respuestasApi.ErrorMessages.Add("El nombre de usuario o password son incorrectos");
                return BadRequest(_respuestasApi);
            }

            _respuestasApi.StatusCode = HttpStatusCode.OK;
            _respuestasApi.IsSuccess = true;
            _respuestasApi.Result = respuestaLogin;
            return Ok(_respuestasApi);
        }

        [Authorize(Roles = "admin,psicologo")]
        [HttpPatch("ActualizarPsicologo/{userId}")]
        [ProducesResponseType(201, Type = typeof(UsuarioActualizarPsicologoDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> ActualizarPsicologo(string userId, [FromBody] UsuarioActualizarPsicologoDto psicologoActualizarDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_usRepo.GetUsuario(userId) == null || psicologoActualizarDto == null)
            {
                return BadRequest(ModelState);
            }

            var usuario = _usRepo.GetUsuario(userId);
            var currentUser = _usRepo.GetCurrentUser();

            if (usuario.Id != currentUser.Id && !User.IsInRole("admin"))
            {
                _respuestasApi.StatusCode = HttpStatusCode.Forbidden;
                _respuestasApi.IsSuccess = false;
                _respuestasApi.ErrorMessages.Add("Error en la actualización de los datos. No puede actualizar datos que no le pertenecen.");
                return StatusCode(403, _respuestasApi);
            }

            var usuarioPsicologo = _mapper.Map<Usuarios>(psicologoActualizarDto);
            if (await _usRepo.ActualizarUsuarioPsicologo(usuarioPsicologo) == null)
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando al actualizar su perfil de usuario");
                return StatusCode(500, ModelState);
            }

            _respuestasApi.StatusCode = HttpStatusCode.OK;
            _respuestasApi.IsSuccess = true;
            return Ok(_respuestasApi);
        }

        [Authorize(Roles = "admin,usuario")]
        [HttpPatch("ActualizarUsuario/{userId}")]
        [ProducesResponseType(201, Type = typeof(UsuarioActualizarDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> ActualizarUsuario(string userId, [FromBody] UsuarioActualizarDto usuarioActualizarDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_usRepo.GetUsuario(userId) == null || usuarioActualizarDto == null)
            {
                return BadRequest(ModelState);
            }

            var usuario = _usRepo.GetUsuario(userId);
            var currentUser = _usRepo.GetCurrentUser();

            if (usuario.Id != currentUser.Id && !User.IsInRole("admin"))
            {
                _respuestasApi.StatusCode = HttpStatusCode.Forbidden;
                _respuestasApi.IsSuccess = false;
                _respuestasApi.ErrorMessages.Add("Error en la actualización de los datos. No puede actualizar datos que no le pertenecen.");
                return StatusCode(403, _respuestasApi);
            }

            var usuarioNormal = _mapper.Map<Usuarios>(usuarioActualizarDto);
            if (await _usRepo.ActualizarUsuario(usuarioNormal) == null)
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando al actualizar su perfil de usuario");
                return StatusCode(500, ModelState);
            }

            _respuestasApi.StatusCode = HttpStatusCode.OK;
            _respuestasApi.IsSuccess = true;
            return Ok(_respuestasApi);
        }
    }
}
