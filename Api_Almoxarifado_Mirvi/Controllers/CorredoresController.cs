using Api_Almoxarifado_Mirvi.Models;
using Api_Almoxarifado_Mirvi.Models.ViewModels;
using Api_Almoxarifado_Mirvi.Services;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;
using System.Diagnostics;

namespace Api_Almoxarifado_Mirvi.Controllers
{
    public class CorredoresController : Controller
    {

        private readonly CorredorService _corredorService;
        private readonly AlmoxarifadoService _almoxarifadoService;

        public CorredoresController(CorredorService corredorService, AlmoxarifadoService almoxarifadoService)
        {
            _corredorService = corredorService;
            _almoxarifadoService = almoxarifadoService;
        }

        public async Task<ActionResult> Index()
        {
            var list = await _corredorService.FindAllAsync();
            return View(list);
        }
        
        public async Task<IActionResult> Create()
        {
            var almorifados = await _almoxarifadoService.FindAllAsync();
            var ViewModel = new FormularioCadastroCorredor { Almoxarifados = almorifados };
            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Corredor corredor)
        {
            await _corredorService.InsertAsync(corredor);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nao fornecido" });
            }

            var obj = await _corredorService.FindByIdAsync(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id fornecido nao encontrado" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _corredorService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = await _corredorService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nao fornecido" });
            }

            var obj = await _corredorService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id fornecido nao encontrado" }); ;
            }

            List<Almoxarifado> almoxarifados = await _almoxarifadoService.FindAllAsync();
            FormularioCadastroCorredor viewModel = new FormularioCadastroCorredor { Corredor = obj, Almoxarifados = almoxarifados };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Corredor corredor)
        {
            if (id != corredor.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Os Ids fornecido nao correspondem" });
            }
            try
            {
                await _corredorService.UpdateAsync(corredor);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
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
