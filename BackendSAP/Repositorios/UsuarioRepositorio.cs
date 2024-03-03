using AutoMapper;
using BackendSAP.Data;
using BackendSAP.Modelos;
using BackendSAP.Modelos.Dtos.Usuarios;
using BackendSAP.Repositorios.IRepositorios;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackendSAP.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _bd;
        private string claveSecreta;
        private readonly UserManager<Usuarios> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UsuarioRepositorio(ApplicationDbContext bd, IConfiguration config,
            UserManager<Usuarios> userManager, RoleManager<IdentityRole> roleManager,
            IMapper mapper)
        {
            _bd = bd;
            //Accedes al AppSetings para obtener la clave secreta de tus tokens
            claveSecreta = config.GetValue<string>("ApiSettings:Secreta");
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public Usuarios GetUsuario(string usuarioId)
        {
            return _bd.Usuarios.FirstOrDefault(u => u.Id == usuarioId);
        }

        public ICollection<Usuarios> GetUsuarios()
        {
            return _bd.Usuarios.OrderBy(u => u.Email).ToList();
        }

        public bool IsUniqueUser(string usuario)
        {
            var usuariobd = _bd.Usuarios.FirstOrDefault(u => u.Email == usuario);
            if (usuariobd == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto)
        {
            //var passwordEncrptado = obtenermd5(usuarioLoginDto.Password);
            var usuario = _bd.Usuarios.FirstOrDefault(
               u => u.Email.ToLower() == usuarioLoginDto.Email.ToLower());

            bool isValida = await _userManager.CheckPasswordAsync(usuario, usuarioLoginDto.Password);
            //Validamos si el usuario no existe con la combinación de usuario y contraseña correcta
            if (isValida = false || usuario == null)
            {
                return new UsuarioLoginRespuestaDto()
                {
                    Token = "",
                    Usuario = null
                };
            }
            //Aquí sí existe el usuario, entnces se puede procesar el login
            var roles = await _userManager.GetRolesAsync(usuario);
            var manejadorToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(claveSecreta);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, usuario.Email.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = manejadorToken.CreateToken(tokenDescriptor);

            UsuarioLoginRespuestaDto usuarioLoginRespuestaDto = new UsuarioLoginRespuestaDto()
            {
                Token = manejadorToken.WriteToken(token),
                Usuario = _mapper.Map<UsuarioDatosDto>(usuario)
            };

            return usuarioLoginRespuestaDto;
        }

        public async Task<UsuarioDatosDto> Registro(UsuarioRegistroDto usuarioRegistroDto)
        {

            Usuarios usuario = new Usuarios()
            {
                UserName = usuarioRegistroDto.Email,
                Email = usuarioRegistroDto.Email,
                NormalizedEmail = usuarioRegistroDto.Email.ToUpper(),
                Nombre = usuarioRegistroDto.Nombre,
                Apellido = usuarioRegistroDto.Apellido
            };

            var result = await _userManager.CreateAsync(usuario, usuarioRegistroDto.Password);
            if (result.Succeeded)
            {
                //Solo la primera vez y es para crear los roles
                if (!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult())
                {
                    await _roleManager.CreateAsync(new IdentityRole("admin"));
                    await _roleManager.CreateAsync(new IdentityRole("psicologo"));
                    await _roleManager.CreateAsync(new IdentityRole("usuario"));
                }

                await _userManager.AddToRoleAsync(usuario, "usuario");
                var usuarioRetornado = _bd.Usuarios.FirstOrDefault(u => u.UserName == usuarioRegistroDto.Email);

                return new UsuarioDatosDto()
                {
                    Id = usuarioRetornado.Id,
                    Email = usuarioRegistroDto.Email,
                    Nombre = usuarioRetornado.Nombre,
                    Apellido = usuarioRetornado.Apellido
                };

            }
            return new UsuarioDatosDto();
        }
    }
}
