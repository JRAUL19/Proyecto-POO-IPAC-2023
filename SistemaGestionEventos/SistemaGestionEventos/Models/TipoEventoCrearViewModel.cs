using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestionEventos.Models
{
    [MetadataType(typeof(TipoEvento))]
    public class TipoEventoCrearViewModel : TipoEvento
    {
        public IEnumerable<SelectListItem> TipoEvento { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(maximumLength: 50)]
        public string Nombre { get; set; }

        [StringLength(maximumLength: 1000)]

        public string? Descripcion { get; set; }
    }
}
