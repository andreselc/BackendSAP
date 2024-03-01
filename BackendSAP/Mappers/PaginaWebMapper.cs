using AutoMapper;
using BackendSAP.Modelos;
using BackendSAP.Modelos.Dtos.Estados;

namespace BackendSAP.Mappers
{
    public class PaginaWebMapper: Profile
    {
        public PaginaWebMapper()
        {
            CreateMap<Estados, EstadoDto>().ReverseMap();
            CreateMap<Estados, CrearEstadoDto>().ReverseMap();

        }
    }
}
