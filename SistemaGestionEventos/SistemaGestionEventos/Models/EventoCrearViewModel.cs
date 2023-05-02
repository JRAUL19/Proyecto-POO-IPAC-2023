using Microsoft.AspNetCore.Mvc.Rendering;

namespace SistemaGestionEventos.Models
{
    public class EventoCrearViewModel : Evento
    {
        public IEnumerable<SelectListItem>? TipoEventoList { get; set; }
        public IEnumerable<SelectListItem>? UbicacionList { get; set; }
        public IEnumerable<SelectListItem>? PatrocinadorList { get; set; }

    }
}
