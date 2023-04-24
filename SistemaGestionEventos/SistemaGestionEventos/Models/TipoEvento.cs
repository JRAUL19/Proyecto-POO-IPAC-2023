using System.ComponentModel.DataAnnotations;

namespace SistemaGestionEventos.Models
{
    public class TipoEvento
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required (ErrorMessage = "El {0} es requerido")]
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
    }
}
