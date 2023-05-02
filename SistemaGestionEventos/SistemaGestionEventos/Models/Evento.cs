using System.ComponentModel.DataAnnotations;

namespace SistemaGestionEventos.Models
{
    public class Evento
    {
        public int Id { get; set; }

        //Usuarios
        public int UsuarioId { get; set; }
        public string? Usuarios { get; set; }

        //Nombre evento

        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(maximumLength: 500)]
        public string Nombre { get; set; }

        //id ubicacion y nombre

        [Display(Name ="Ubicacion")]
        public int UbicacionId { get; set; }
        public string? Ubicaciones { get; set; }

        //id tipo evento y nombre

        [Display(Name = "Tipo de evento")]
        public int TipoEventoId { get; set; }
        public string? TiposEventos { get; set; }

        //Fechas de inicio y final

        [Display(Name = "Fecha de inicio")]
        [Required(ErrorMessage = "La {0} es requerida")]
        [RegularExpression(@"^\d{2}/\d{2}/\d{4}$", ErrorMessage = "El formato de fecha debe ser dd/mm/aaaa")]
        [StringLength(maximumLength: 50)]
        public string FechaInicio { get; set; }

        [Display(Name = "Fecha de finalizacion")]
        [Required(ErrorMessage = "La {0} es requerida")]
        [RegularExpression(@"^\d{2}/\d{2}/\d{4}$", ErrorMessage = "El formato de fecha debe ser dd/mm/aaaa")]
        [StringLength(maximumLength: 50)]
        public string FechaFinal { get; set; }

        //Precios de entrada

        [Display(Name ="Precio de entrada")]
        [Required(ErrorMessage = "El {0} es requerido en caso de no tener costo coloque 0")]
        public decimal PrecioDeEntrada { get; set; }

        //id patrocinador y nombre
        [Display(Name = "Patrocinador")]
        public int PatrocinadorId { get; set; }
        public string? Patrocinadores { get; set; }
        //Descripcion

        [StringLength(maximumLength: 2500)]
        public string? Descripcion { get; set; }
    }
}
