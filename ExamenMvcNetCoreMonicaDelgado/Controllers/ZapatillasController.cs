using ExamenMvcNetCoreMonicaDelgado.Models;
using ExamenMvcNetCoreMonicaDelgado.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExamenMvcNetCoreMonicaDelgado.Controllers
{
    public class ZapatillasController : Controller
    {
        private RepositoryZapatillas repositoryZapatillas;

        public ZapatillasController(RepositoryZapatillas repo)
        {
            this.repositoryZapatillas = repo;
        }
        public async Task<IActionResult> Index()
        {
            List<Zapatilla> zapatillas = await this.repositoryZapatillas.GetZapatillasAsync();
            return View(zapatillas);
        }

        public async Task<IActionResult> Detalles(int idzapatilla)
        {
            Zapatilla zapatilla = await this.repositoryZapatillas.FindZapatillaByIdAsync(idzapatilla);
            return View(zapatilla);
        }

        public async Task<IActionResult> _ImagenesPartial(int? posicion, int idzapatilla)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            Zapatilla zapa = await this.repositoryZapatillas.FindZapatillaByIdAsync(idzapatilla);
            Imagen imagen = await this.repositoryZapatillas.GetImagenByPosAsync(idzapatilla, posicion.Value);
            

            int numRegistros = await this.repositoryZapatillas.GetNumeroImagenesByIdZapatillaAsync(idzapatilla);

            int siguiente = posicion.Value + 1;

            if (siguiente > numRegistros)
            {
                siguiente = numRegistros;
            }

            int anterior = posicion.Value - 1;
            if (anterior < 1)
            {
                anterior = 1;
            }

            ViewData["ULTIMO"] = numRegistros;
            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;
            ViewData["POSICION"] = posicion;

            return PartialView("_ImagenesPartial",imagen);
        }
    }
}
