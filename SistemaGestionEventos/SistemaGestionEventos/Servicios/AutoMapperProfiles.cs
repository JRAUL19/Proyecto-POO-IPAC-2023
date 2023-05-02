using AutoMapper;
using SistemaGestionEventos.Models;

namespace SistemaGestionEventos.Servicios
{
    public class AutoMapperProfiles : Profile
    {
        //Esto es un servicio para el mapeo de objetos

        public AutoMapperProfiles()
        {
            CreateMap<TipoEvento, TipoEventoCrearViewModel>();

            CreateMap<Ubicacion, UbicacionEditarViewModel>();

            CreateMap<Evento, EventoCrearViewModel>();
        }
    }
}
