using Api_Almoxarifado_Mirvi.Models;
using Api_Almoxarifado_Mirvi.Models.ViewModels;
using Api_Almoxarifado_Mirvi.Services;
using Api_Almoxarifado_Mirvi.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Api_Almoxarifado_Mirvi.Controllers
{
    public class PrateleirasController : Controller
    {

        private readonly PrateleiraService _prateleiraService;
        private readonly CorredorService _corredorService;

        public PrateleirasController(PrateleiraService prateleiraService, CorredorService corredorService)
        {
            _prateleiraService = prateleiraService;
            _corredorService = corredorService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _prateleiraService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var corredores = await _corredorService.FindAllAsync();
            var viewModel = new FormularioCadastroPrateleira { Corredores = corredores };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Prateleira prateleira)
        {
            if (!ModelState.IsValid)
            {
                await _prateleiraService.InsertAsync(prateleira);
                return RedirectToAction(nameof(Index));
            }
            var corredores = await _corredorService.FindAllAsync();
            var viewModel = new FormularioCadastroPrateleira { Corredores = corredores, Prateleira = prateleira };
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nao fornecido" });
            }

            var obj = await _prateleiraService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id fornecido nao encontrado" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _prateleiraService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegreityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nao fornecido" });
            }

            var obj = await _prateleiraService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id fornecido nao encontrado" });
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nao fornecido" });
            }

            var obj = await _prateleiraService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id fornecido nao encontrado" }); ;
            }

            List<Corredor> corredores = await _corredorService.FindAllAsync();
            FormularioCadastroPrateleira viewModel = new FormularioCadastroPrateleira { Prateleira = obj, Corredores = corredores};
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Prateleira prateleira)
        {
            if (!ModelState.IsValid)
            {
                var corredores = await _corredorService.FindAllAsync();
                var viewModel = new FormularioCadastroPrateleira { Corredores = corredores, Prateleira = prateleira };
            }
            if (id != prateleira.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Os Ids fornecido nao correspondem" });
            }
            try
            {
                await _prateleiraService.UpdateAsync(prateleira);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
