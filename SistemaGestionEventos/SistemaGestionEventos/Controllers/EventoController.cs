using AutoMapper;
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
        private readonly IMapper mapper;

        public EventoController
            (IRepositorioPatrocinador repositorioPatrocinador,
            IRepositorioTipoEvento repositorioTipoEvento,
            IRepositorioUbicacion repositorioUbicacion,
            IRepositorioEvento repositorioEvento,
            ILogger<HomeController> logger,
            IMapper mapper)
        {
            this.repositorioPatrocinador = repositorioPatrocinador;
            this.repositorioTipoEvento = repositorioTipoEvento;
            this.repositorioUbicacion = repositorioUbicacion;
            this.repositorioEvento = repositorioEvento;
            _logger = logger;
            this.mapper = mapper;
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
                return RedirectToAction("NotFound", "Home");
            }

            await repositorioEvento.Eliminar(id);

            return RedirectToAction("Index");

        }

        //Editar un evento
        public async Task<IActionResult> Editar(int id)
        {
            var evento = await repositorioEvento.ObtenerId(id);

            if (evento is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var modelo = mapper.Map<EventoCrearViewModel>(evento);
            //var modelo = new EventoCrearViewModel
            //{
            //    Id = evento.Id,
            //    Nombre = evento.Nombre,
            //    UbicacionId = evento.UbicacionId,
            //    TipoEventoId = evento.TipoEventoId,
            //    FechaInicio = evento.FechaInicio,
            //    FechaFinal = evento.FechaFinal,
            //    PrecioDeEntrada = evento.PrecioDeEntrada,
            //    PatrocinadorId = evento.PatrocinadorId,
            //    Descripcion = evento.Descripcion,
            //};

            modelo.TipoEventoList = await ObtenerTipoEventos();
            modelo.UbicacionList = await ObtenerUbicaciones();
            modelo.PatrocinadorList = await ObtenerPatrocinadores();


            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(EventoCrearViewModel modelo)
        {
            var evento = await repositorioEvento.ObtenerId(modelo.Id);

            if (!ModelState.IsValid)
            {
                modelo.TipoEventoList = await ObtenerTipoEventos();
                modelo.UbicacionList = await ObtenerUbicaciones();
                modelo.PatrocinadorList = await ObtenerPatrocinadores();
                return View(modelo);
            }

            if (evento is null)
            {
                return RedirectToAction("NotFound", "Home");
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

            await repositorioEvento.Editar(modelo);

            return RedirectToAction("Index");
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
