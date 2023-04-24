using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionEventos.Models;
using SistemaGestionEventos.Servicios;

namespace SistemaGestionEventos.Controllers
{
    public class TipoEventoController : Controller
    {
        private readonly IRepositorioTipoEvento repositorioTipoEvento;

        public TipoEventoController(IRepositorioTipoEvento repositorioTipoEvento
            )
        {
            this.repositorioTipoEvento = repositorioTipoEvento;
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
    }
}
