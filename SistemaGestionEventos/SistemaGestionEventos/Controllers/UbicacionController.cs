using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionEventos.Models;
using SistemaGestionEventos.Servicios;

namespace SistemaGestionEventos.Controllers
{
    public class UbicacionController : Controller
    {
        private readonly IRepositorioUbicacion repositorioUbicacion;
        private readonly IMapper mapper;
        public UbicacionController(IRepositorioUbicacion repositorioUbicacion, IMapper mapper)
        {
            this.repositorioUbicacion = repositorioUbicacion;
            this.mapper = mapper;
        }

        //Vista principal de ubicaciones
        public async Task<IActionResult> Index()
        {
            var ubicacion = await repositorioUbicacion.Obtener();
            return View(ubicacion);
        }

        //Funcion de agregar nueva ubicacion
        [HttpGet]
        public IActionResult Crear() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Ubicacion ubicacion) 
        {
            if (!ModelState.IsValid) 
            {
                return View(ubicacion);
            }

            var ubicacionExiste = await repositorioUbicacion.Existe(ubicacion.Nombre);

            if (ubicacionExiste) 
            {
                ModelState.AddModelError(nameof(ubicacion.Nombre), $"El nombre {ubicacion.Nombre} ya existe");
                return View(ubicacion);
            }

            await repositorioUbicacion.Crear(ubicacion);
            return RedirectToAction("Index");
        }

        //Funcion de eliminar una ubicacion.
        [HttpGet]
        public async Task<IActionResult> Eliminar(int id) 
        {
            var ubicacion = await repositorioUbicacion.ObtenerId(id);

            if (ubicacion is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(ubicacion);
        }

        [HttpPost]

        public async Task<IActionResult> EliminarConfirm (int id) 
        {
            var ubicacion = await repositorioUbicacion.ObtenerId(id);

            if (ubicacion is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioUbicacion.Eliminar(id);
            return RedirectToAction("Index");
        }

        //Funcion editar una ubicacion
        [HttpGet]
        public async Task<IActionResult> Editar(int id) 
        {
            var ubicacion = await repositorioUbicacion.ObtenerId(id);

            var editarUbicacion = new UbicacionEditarViewModel
            {
                Id = ubicacion.Id,
                Nombre = ubicacion.Nombre,
                Direccion = ubicacion.Direccion,
                DireccionUrl = ubicacion.DireccionUrl,
                CapacidadPersonas = ubicacion.CapacidadPersonas,
                Servicios = ubicacion.Servicios,
                Descripcion = ubicacion.Descripcion,
            };

            if (ubicacion is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(editarUbicacion);
        }

        [HttpPost]

        public async Task<IActionResult> Editar(UbicacionEditarViewModel editarUbicacion) 
        {
            if (!ModelState.IsValid)
            {
                return View(editarUbicacion);
            }

            if (editarUbicacion is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await repositorioUbicacion.Editar(editarUbicacion);
            return RedirectToAction("Index");
        }
    }
}
