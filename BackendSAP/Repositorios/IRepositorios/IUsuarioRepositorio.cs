﻿using BackendSAP.Modelos;
using BackendSAP.Modelos.Dtos.Usuarios;

namespace BackendSAP.Repositorios.IRepositorios
{
    public interface IUsuarioRepositorio
    {
        ICollection<Usuarios> GetUsuarios();
        Usuarios GetUsuario(string usuarioId);
        bool IsUniqueUser(string usuario);

        bool IsAdminUser(string usuario);
        Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto);
        Task<UsuarioDatosDto> Registro(UsuarioRegistroDto usuarioRegistroDto);
    }
}
