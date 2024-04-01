using AutoMapper;
using BackendSAP.Modelos;
using BackendSAP.Modelos.Dtos.Calificaciones;
using BackendSAP.Modelos.Dtos.Ciudades;
using BackendSAP.Modelos.Dtos.Estados;
using BackendSAP.Modelos.Dtos.Trastornos;
using BackendSAP.Modelos.Dtos.Usuarios;

namespace BackendSAP.Mappers
{
    public class PaginaWebMapper: Profile
    {
        public PaginaWebMapper()
        {
            CreateMap<Estados, EstadoDto>().ReverseMap();
            CreateMap<Estados, CrearEstadoDto>().ReverseMap();
            CreateMap<Ciudades, CiudadDto>().ReverseMap();
            CreateMap<Ciudades, CrearCiudadDto>().ReverseMap();
            CreateMap<Usuarios, UsuarioDto>().ReverseMap();
            CreateMap<Usuarios, UsuarioDatosDto>().ReverseMap();
            CreateMap<Usuarios, UsuarioActualizarDto>().ReverseMap();
            CreateMap<Usuarios, UsuarioActualizarPsicologoDto>().ReverseMap();
            CreateMap<Usuarios, UsuarioPsicologoDto>().ReverseMap();
            CreateMap<TrastornoPsicologico, TrastornoDto>().ReverseMap();
            CreateMap<TrastornoPsicologico, CrearTrastornoDto>().ReverseMap();
            CreateMap<TrastornoPsicologico, ActualizarTrastornoDto>().ReverseMap();
            CreateMap<Calificaciones, CalificacionesDto>().ReverseMap();
            CreateMap<Calificaciones, CrearCalificacionesDto>().ReverseMap();
            CreateMap<Calificaciones, ActualizarCalificacionesDto>().ReverseMap();
        }
    }
}
