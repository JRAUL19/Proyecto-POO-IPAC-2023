using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaGestionEventos.Models;
using SistemaGestionEventos.Servicios;

namespace SistemaGestionEventos.Controllers
{
    public class EventoController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepositorioEvento repositorioEvento;
        private readonly IRepositorioPatrocinador repositorioPatrocinador;
        private readonly IRepositorioTipoEvento repositorioTipoEvento;
        private readonly IRepositorioUbicacion repositorioUbicacion;

        public EventoController
            (IRepositorioPatrocinador repositorioPatrocinador,
            IRepositorioTipoEvento repositorioTipoEvento,
            IRepositorioUbicacion repositorioUbicacion,
            IRepositorioEvento repositorioEvento,
            ILogger<HomeController> logger)
        {
            this.repositorioPatrocinador = repositorioPatrocinador;
            this.repositorioTipoEvento = repositorioTipoEvento;
            this.repositorioUbicacion = repositorioUbicacion;
            this.repositorioEvento = repositorioEvento;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var evento = await repositorioEvento.Obtener();
            return View(evento);
        }

        //Crear un nuevo evento
        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var modelo = new EventoCrearViewModel();

            modelo.TipoEventoList = await ObtenerTipoEventos();
            modelo.UbicacionList = await ObtenerUbicaciones();
            modelo.PatrocinadorList = await ObtenerPatrocinadores();

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(EventoCrearViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                modelo.TipoEventoList = await ObtenerTipoEventos();
                modelo.UbicacionList = await ObtenerUbicaciones();
                modelo.PatrocinadorList = await ObtenerPatrocinadores();
                return View(modelo);
            }

            var tipoEvento = await repositorioTipoEvento.ObtenerPorId(modelo.TipoEventoId);
            if (tipoEvento is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var ubicacion = await repositorioUbicacion.ObtenerId(modelo.UbicacionId);
            if (ubicacion is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var patrocinador = await repositorioPatrocinador.ObtenerId(modelo.PatrocinadorId);
            if (patrocinador is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorioEvento.Crear(modelo);

            return RedirectToAction("Index");

        }

        //Eliminar un evento
        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var evento = await repositorioEvento.ObtenerId(id);

            if (evento is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(evento);

        }

        [HttpPost]
        public async Task<IActionResult> EliminarConfirm(int id)
        {
            var evento = await repositorioEvento.ObtenerId(id);

            if (evento is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioEvento.Eliminar(id);

            return RedirectToAction("Index");

        }

        //Editar un evento
        public IActionResult Editar()
        {
            return View();
        }

        //Obtener datos de tipoEvento Ubicaciones y patrocinadores
        private async Task<IEnumerable<SelectListItem>> ObtenerTipoEventos()
        {
            var tiposEvento = await repositorioTipoEvento.Obtener();
            return tiposEvento.Select(x => new SelectListItem(x.Nombre, x.Id.ToString()));
        }

        private async Task<IEnumerable<SelectListItem>> ObtenerUbicaciones()
        {
            var ubicaciones = await repositorioUbicacion.Obtener();
            return ubicaciones.Select(x => new SelectListItem(x.Nombre, x.Id.ToString()));
        }

        private async Task<IEnumerable<SelectListItem>> ObtenerPatrocinadores()
        {
            var patrocinadores = await repositorioPatrocinador.Obtener();
            return patrocinadores.Select(x => new SelectListItem(x.Nombre, x.Id.ToString()));
        }
    }
}
