using BackendSAP.Modelos;
using BackendSAP.Modelos.Dtos.Usuarios;

namespace BackendSAP.Repositorios.IRepositorios
{
    public interface IUsuarioRepositorio
    {
        ICollection<Usuarios> GetUsuarios();
        Usuarios GetUsuario(string usuarioId);
        bool IsUniqueUser(string usuario);
        Usuarios GetCurrentUser();
        ICollection<Usuarios> BuscarUsuarioPorNombre(string nombre);
        Task<Usuarios> ActualizarUsuarioPsicologo(Usuarios usuario);
        Task<Usuarios> ActualizarUsuario(Usuarios usuario);
        Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto);
        Task<UsuarioDatosDto> Registro(UsuarioRegistroDto usuarioRegistroDto);
    }
}
