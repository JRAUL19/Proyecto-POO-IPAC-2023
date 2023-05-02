using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionEventos.Models;
using SistemaGestionEventos.Servicios;
using System.Reflection;

namespace SistemaGestionEventos.Controllers
{
    public class TipoEventoController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepositorioTipoEvento repositorioTipoEvento;
        private readonly IMapper mapper;


        public TipoEventoController(
            IRepositorioTipoEvento repositorioTipoEvento,
            IMapper mapper
,
            ILogger<HomeController> logger)
        {
            this.repositorioTipoEvento = repositorioTipoEvento;
            this.mapper = mapper;
            _logger = logger;
        }

        //Vista principal
        public async Task<IActionResult> Index() 
        {
            var tipoEvento = await repositorioTipoEvento.Obtener();
            return View(tipoEvento);
        }

        //Crear Nuevo Tipo Evento
        [HttpGet]
        public IActionResult Crear() 
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(TipoEvento tipoEvento) 
        {
            if (!ModelState.IsValid) 
            {
                return View(tipoEvento);
            }

            var tipoEventoExiste = await repositorioTipoEvento.Existe(tipoEvento.Nombre);

            if (tipoEventoExiste) 
            {
                ModelState.AddModelError(nameof(tipoEvento.Nombre), $"El nombre {tipoEvento.Nombre} ya existe");
                return View(tipoEvento);
            }

            _logger.LogInformation("Modelo recibido: {Modelo}", tipoEvento.Nombre);


            await repositorioTipoEvento.Crear(tipoEvento);
            return RedirectToAction("Index");   
        }

        //Eliminar Tipo Evento
        [HttpGet]
        public async Task<IActionResult> Eliminar(int id) 
        {
            var tipoCuenta = await repositorioTipoEvento.ObtenerPorId(id);

            if (tipoCuenta is null) 
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(tipoCuenta);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarConfirm(int id) 
        {
            var tipoEvento = await repositorioTipoEvento.ObtenerPorId(id);

            if (tipoEvento is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioTipoEvento.Eliminar(id);
            return RedirectToAction("Index");
        }

        //Editar un tipo de evento
        [HttpGet]
        public async Task<IActionResult> Editar(int id) 
        {
            var tipoEvento = await repositorioTipoEvento.ObtenerPorId(id);

            //var modelo = mapper.Map<TipoEventoCrearViewModel>(tipoEvento);
            var editarTipoEvento = new EditarTipoEventoViewModel
            {
                Id = tipoEvento.Id,
                Nombre = tipoEvento.Nombre,
                Descripcion = tipoEvento.Descripcion,
            };

            if (tipoEvento is null)
            {
                return View("NotFound", "Home");
            }

            return View(editarTipoEvento);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(EditarTipoEventoViewModel editarTipoEvento) 
        {
            if (!ModelState.IsValid)
            {
                return View(editarTipoEvento);
            }

            await repositorioTipoEvento.Editar(editarTipoEvento);

            return RedirectToAction("Index");
        }

    }
}
