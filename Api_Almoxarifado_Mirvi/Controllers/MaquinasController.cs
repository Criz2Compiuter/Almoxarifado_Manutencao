using Api_Almoxarifado_Mirvi.Models;
using Api_Almoxarifado_Mirvi.Models.ViewModels;
using Api_Almoxarifado_Mirvi.Services;
using Api_Almoxarifado_Mirvi.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Api_Almoxarifado_Mirvi.Controllers
{
    public class MaquinasController : Controller
    {

        private readonly MaquinasService _maquinaService;
        private readonly CorredorService _corredorService;
        private readonly AlmoxarifadoService _almoxarifadoService;

        public MaquinasController(MaquinasService maquinaService, CorredorService corredorService, AlmoxarifadoService almoxarifadoService)
        {
            _maquinaService = maquinaService;
            _corredorService = corredorService;
            _almoxarifadoService = almoxarifadoService;
        }

        public async Task<IActionResult> Index(int almoxarifadoId)
        {
            ViewBag.AlmoxarifadoId = almoxarifadoId;
            var list = await _maquinaService.FindAllInAlmoxarifadoAsync(almoxarifadoId);
            return View(list);
        }

        public async Task<IActionResult> Create(int almoxarifadoId)
        {
            ViewBag.AlmoxarifadoId = almoxarifadoId;
            var almoxarifado = await _almoxarifadoService.FindAllAsync();
            var viewModel = new FormularioCadastroMaquina 
            {
                Almoxarifados = almoxarifado,
                AlmoxarifadoId = almoxarifadoId,
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Maquina maquina, int almoxarifadoId)
        {
            if (!ModelState.IsValid)
            {
                await _maquinaService.InsertAsync(maquina);
                return RedirectToAction(nameof(Index), new { almoxarifadoId });
            }
            var almoxarifado = await _almoxarifadoService.FindAllAsync();
            var viewModel = new FormularioCadastroMaquina 
            { 
                Maquina = maquina,
                Almoxarifados = almoxarifado,
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

            var obj = await _maquinaService.FindByIdAsync(id.Value);
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
                await _maquinaService.RemoveAsync(id);
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

            var obj = await _maquinaService.FindByIdAsync(id.Value);
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

            var obj = await _maquinaService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id fornecido nao encontrado" }); ;
            }

            List<Almoxarifado> almoxarifado = await _almoxarifadoService.FindAllAsync();
            FormularioCadastroMaquina viewModel = new FormularioCadastroMaquina 
            { 
                Maquina = obj,
                Almoxarifados = almoxarifado,
                AlmoxarifadoId = almoxarifadoId
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Maquina maquina, int almoxarifadoId)
        {
            if (ModelState.IsValid)
            {
                var almoxarifados = await _almoxarifadoService.FindAllAsync();
                var viewModel = new FormularioCadastroMaquina 
                {
                    Almoxarifados = almoxarifados,
                    Maquina = maquina, 
                    AlmoxarifadoId = almoxarifadoId
                };
            }
            if (id != maquina.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Os Ids fornecido nao correspondem" });
            }
            try
            {
                await _maquinaService.UpdateAsync(maquina);
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
