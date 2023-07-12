using Api_Almoxarifado_Mirvi.Models;
using Api_Almoxarifado_Mirvi.Models.ViewModels;
using Api_Almoxarifado_Mirvi.Services;
using Api_Almoxarifado_Mirvi.Services.Exceptions;
using Humanizer.Localisation.TimeToClockNotation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Index(int almoxarifadoId)
        {
            ViewBag.AlmoxarifadoId = almoxarifadoId;
            var list = await _corredorService.FindAllInAlmoxarifadoAsync(almoxarifadoId);
            return View(list);
        }

        public async Task<IActionResult> Create(int almoxarifadoId)
        {
            ViewBag.AlmoxarifadoId = almoxarifadoId;
            var almoxarifados = await _almoxarifadoService.FindAllAsync();
            var ViewModel = new FormularioCadastroCorredor 
            {
                Almoxarifados = almoxarifados,
                AlmoxarifadoId = almoxarifadoId 
            };
            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Corredor corredor, int almoxarifadoId)
        {
            if (!ModelState.IsValid)
            {
                await _corredorService.InsertAsync(corredor);
                return RedirectToAction(nameof(Index), new {almoxarifadoId});
            }
            var almoxarifados = await _almoxarifadoService.FindAllAsync();
            var viewModel = new FormularioCadastroCorredor
            {
                Almoxarifados = almoxarifados,
                AlmoxarifadoId = almoxarifadoId
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id, int almoxarifadoId)
        {
            ViewBag.AlmoxarifadoId = almoxarifadoId;
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nao fornecido" });
            }

            var obj = await _corredorService.FindByIdAsync(id.Value);
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
                await _corredorService.RemoveAsync(id);
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

            var obj = await _corredorService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nao encontrado" });
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

            var obj = await _corredorService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id fornecido nao encontrado" }); ;
            }

            List<Almoxarifado> almoxarifados = await _almoxarifadoService.FindAllAsync();
            FormularioCadastroCorredor viewModel = new FormularioCadastroCorredor
            {
                Corredor = obj,
                Almoxarifados = almoxarifados,
                AlmoxarifadoId = almoxarifadoId
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Corredor corredor, int almoxarifadoId)
        {
            if (ModelState.IsValid)
            {
                var almoxarifado = await _almoxarifadoService.FindAllAsync();
                var viewModel = new FormularioCadastroCorredor
                {
                    Almoxarifados = almoxarifado,
                    Corredor = corredor,
                    AlmoxarifadoId = almoxarifadoId
                };
                return View(viewModel);
            }
            if (id != corredor.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Os Ids fornecido nao correspondem" });
            }
            try
            {
                await _corredorService.UpdateAsync(corredor);
                return RedirectToAction(nameof(Index), new { almoxarifadoId });
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