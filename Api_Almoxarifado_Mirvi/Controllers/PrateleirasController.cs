using Api_Almoxarifado_Mirvi.Models;
using Api_Almoxarifado_Mirvi.Models.ViewModels;
using Api_Almoxarifado_Mirvi.Services;
using Api_Almoxarifado_Mirvi.Services.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Api_Almoxarifado_Mirvi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PrateleirasController : Controller
    {

        private readonly PrateleiraService _prateleiraService;
        private readonly CorredorService _corredorService;
        private readonly AlmoxarifadoService _almoxarifadoService;

        public PrateleirasController(PrateleiraService prateleiraService, CorredorService corredorService, AlmoxarifadoService almoxarifadoService)
        {
            _prateleiraService = prateleiraService;
            _corredorService = corredorService;
            _almoxarifadoService = almoxarifadoService;
        }

        public async Task<IActionResult> Index(int almoxarifadoId)
        {
            ViewBag.AlmoxarifadoId = almoxarifadoId;
            var list = await _prateleiraService.FindAllInAlmoxarifadoAsync(almoxarifadoId);
            return View(list);
        }

        public async Task<IActionResult> Create(int almoxarifadoId)
        {
            ViewBag.AlmoxarifadoId = almoxarifadoId;
            var corredores = await _corredorService.FindAllAsync();
            var almoxarifado = await _almoxarifadoService.FindAllAsync();
            var viewModel = new FormularioCadastroPrateleira 
            { 
                Corredores = corredores,
                Almoxarifados = almoxarifado 
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Prateleira prateleira, int almoxarifadoId)
        {
            if (!ModelState.IsValid)
            {
                var corredores = await _corredorService.FindAllAsync();
                var almoxarifado = await _almoxarifadoService.FindAllAsync();
                var viewModel = new FormularioCadastroPrateleira 
                {
                    Corredores = corredores,
                    Prateleira = prateleira,
                    Almoxarifados = almoxarifado,
                    AlmoxarifadoId = almoxarifadoId
                };
                return View(viewModel);
            }
            await _prateleiraService.InsertAsync(prateleira);
            return RedirectToAction(nameof(Index), new { almoxarifadoId });
        }

        public async Task<IActionResult> Delete(int? id, int almoxarifadoId)
        {
            ViewBag.AlmoxarifadoId = almoxarifadoId;
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
        public async Task<IActionResult> Delete(int id, int almoxarifadoId)
        {
            try
            {
                await _prateleiraService.RemoveAsync(id);
                return RedirectToAction(nameof(Index), new { almoxarifadoId });
            }
            catch (IntegreityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Details(int? id, int almoxarifadoId)
        {
            ViewBag.AlmoxarifadoId = almoxarifadoId;
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

        public async Task<IActionResult> Edit(int? id, int almoxarifadoId)
        {
            ViewBag.AlmoxarifadoId = almoxarifadoId;
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nao fornecido" });
            }

            var obj = await _prateleiraService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id fornecido nao encontrado" }); ;
            }

            List<Almoxarifado> almoxarifado = await _almoxarifadoService.FindAllAsync();
            List<Corredor> corredores = await _corredorService.FindAllAsync();
            FormularioCadastroPrateleira viewModel = new FormularioCadastroPrateleira 
            {
                Prateleira = obj,
                Corredores = corredores,
                AlmoxarifadoId = almoxarifadoId,
                Almoxarifados = almoxarifado 
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Prateleira prateleira, int almoxarifadoId)
        {
            if (ModelState.IsValid)
            {
                var almoxarifado = await _almoxarifadoService.FindAllAsync();
                var corredores = await _corredorService.FindAllAsync();
                var viewModel = new FormularioCadastroPrateleira
                {
                    Corredores = corredores,
                    Prateleira = prateleira,
                    Almoxarifados = almoxarifado,
                    AlmoxarifadoId = almoxarifadoId
                };
                return View(viewModel);
            }
            if (id != prateleira.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Os Ids fornecido nao correspondem" });
            }
            try
            {
                await _prateleiraService.UpdateAsync(prateleira);
                return RedirectToAction(nameof(Index), new { almoxarifadoId });
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
