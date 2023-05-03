using System.ComponentModel.DataAnnotations;

namespace SistemaGestionEventos.Models
{
    public class Ubicacion
    {
        public int Id { get; set; }

        [Display(Name ="Nombre")]
        [Required (ErrorMessage = "El {0} es requerido")]
        [StringLength(maximumLength:100)]
        public string Nombre { get; set; }

        [Required (ErrorMessage = "La {0} es requerida")] 
        [StringLength(maximumLength:500)]
        public string Direccion { get; set; }

        [Display(Name = "Url de la direccion o Web de la ubicacion")]
        [Url]
        [StringLength(maximumLength:2000)]
        public string? DireccionUrl { get; set; }

        [Display(Name ="Capacidad de personas del lugar")]
        [Required(ErrorMessage = "La {0} es requerida")]
        [StringLength(maximumLength:100)]
        [RegularExpression(@"^\d+$", ErrorMessage = "El campo {0} solo permite números enteros.")]
        public string CapacidadPersonas { get; set; }

        [Display(Name = "Servicios que ofrece el lugar")]
        [Required(ErrorMessage = "Los {0} son requeridos")]
        [StringLength(maximumLength:1500)]
        public string Servicios { get; set; }

        [StringLength(maximumLength:1500)]
        public string? Descripcion { get; set; }
    }
}
