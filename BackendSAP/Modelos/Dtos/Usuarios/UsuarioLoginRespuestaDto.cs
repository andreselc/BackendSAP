namespace BackendSAP.Modelos.Dtos.Usuarios
{
    public class UsuarioLoginRespuestaDto
    {
        public UsuarioDatosDto Usuario { get; set; }
        public string Token { get; set; }

        public string Role { get; set; }
    }
}
