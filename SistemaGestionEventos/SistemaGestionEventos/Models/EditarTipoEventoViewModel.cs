using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestionEventos.Models
{
    public class EditarTipoEventoViewModel : TipoEvento
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(maximumLength: 50)]
        public string Nombre { get; set; }

        [StringLength(maximumLength: 1000)]
        public string? Descripcion { get; set; }
    }
}
