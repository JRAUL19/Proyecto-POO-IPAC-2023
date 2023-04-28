using Microsoft.AspNetCore.Mvc;
using SistemaGestionEventos.Models;
using SistemaGestionEventos.Servicios;

namespace SistemaGestionEventos.Controllers
{
    public class PatrocinadorController : Controller
    {
        private readonly IRepositorioPatrocinador repositorioPatrocinador;
        public PatrocinadorController(IRepositorioPatrocinador repositorioPatrocinador)
        {
            this.repositorioPatrocinador = repositorioPatrocinador;
        }

        //Vista Principal
        public async Task<IActionResult> IndexAsync()
        {
            var patrocinador = await repositorioPatrocinador.Obtener();
            return View(patrocinador);
        }

        //Crear Nuevo Patrocinador
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Patrocinador patrocinador)
        {
            if (!ModelState.IsValid)
            {
                return View(patrocinador);
            }

            var ubicacionExiste = await repositorioPatrocinador.Existe(patrocinador.Nombre);
            patrocinador.EmailNormalisado = emailNorm(patrocinador.Email);

            if (ubicacionExiste)
            {
                ModelState.AddModelError(nameof(patrocinador.Nombre), $"El nombre {patrocinador.Nombre} ya existe");
                return View(patrocinador);
            }

            await repositorioPatrocinador.Crear(patrocinador);
            return RedirectToAction("Index");
        }

        //Eliminar Patrocinador
        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var patrcinador = await repositorioPatrocinador.ObtenerId(id);

            if (patrcinador is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(patrcinador);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarConfirm(int id)
        {
            var patrocinador = await repositorioPatrocinador.ObtenerId(id);

            if (patrocinador is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioPatrocinador.Eliminar(id);
            return RedirectToAction("Index");
        }

        //Editar Patrocinador
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var patrocinador = await repositorioPatrocinador.ObtenerId(id);

            var editarPatrocinador = new EditarPatrocinadorViewModel
            {
                Id = patrocinador.Id,
                Nombre = patrocinador.Nombre,
                Email = patrocinador.Email,
                EmailNormalisado = patrocinador.EmailNormalisado,
                Telefono = patrocinador.Telefono,
                Direccion = patrocinador.Direccion,
                Descripcion = patrocinador.Descripcion,
            };

            if (patrocinador is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(editarPatrocinador);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(EditarPatrocinadorViewModel editarPatrocinador)
        {

            if (!ModelState.IsValid)
            {
                return View(editarPatrocinador);
            }

            if (editarPatrocinador is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            editarPatrocinador.EmailNormalisado = emailNorm(editarPatrocinador.Email);

            await repositorioPatrocinador.Editar(editarPatrocinador);
            return RedirectToAction("Index");
        }


        //Funciones Extra
        string emailNorm(string email)
        {
            string emailNormalisado = email.ToUpper();
            return emailNormalisado;
        }
    }
}
