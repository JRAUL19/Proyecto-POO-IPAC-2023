using System.ComponentModel.DataAnnotations;

namespace SistemaGestionEventos.Models
{
    public class EditarPatrocinadorViewModel : Patrocinador
    {

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(maximumLength: 100)]
        public string Nombre { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(maximumLength: 500)]
        public string? Email { get; set; }

        [StringLength(maximumLength: 500)]
        public string? EmailNormalisado { get; set; }

        [Display(Name = "Telefono")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(maximumLength: 200)]
        public string Telefono { get; set; }

        [Display(Name = "Direccion")]
        [Required(ErrorMessage = "La {0} es requerida")]
        [StringLength(maximumLength: 1000)]
        public string Direccion { get; set; }

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "La {0} es requerida")]
        [StringLength(maximumLength: 1000)]
        public string Descripcion { get; set; }

    }
}
