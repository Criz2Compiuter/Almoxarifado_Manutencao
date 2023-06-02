using Api_Almoxarifado_Mirvi.Models;
using Api_Almoxarifado_Mirvi.Models.ViewModels;
using Api_Almoxarifado_Mirvi.Services;
using Api_Almoxarifado_Mirvi.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Api_Almoxarifado_Mirvi.Controllers
{
    public class EnderecosController : Controller
    {

        private readonly EnderecoService _enderecoService;
        private readonly PrateleiraService _prateleiraService;

        public EnderecosController(EnderecoService enderecosService, PrateleiraService prateleiraService)
        {
            _enderecoService = enderecosService;
            _prateleiraService = prateleiraService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _enderecoService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var prateleiras = await _prateleiraService.FindAllAsync();
            var viewModel = new FormularioCadastroEndereco { Prateleira = prateleiras };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Endereco endereco)
        {
            if (!ModelState.IsValid)
            {
                var prateleiras = await _prateleiraService.FindAllAsync();
                var viewModel = new FormularioCadastroEndereco { Prateleira = prateleiras, Endereco = endereco };
                return View(viewModel);
            }
            await _enderecoService.InsertAsync(endereco);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nao fornecido" });
            }

            var obj = await _enderecoService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nao encontrado" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _enderecoService.RemoveAsync(id);
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

            var obj = await _enderecoService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nao encontrado" });
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nao fornecido" });
            }

            var obj = await _enderecoService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id fornecido nao encontrado" }); ;
            }

            List<Prateleira> prateleiras = await _prateleiraService.FindAllAsync();
            FormularioCadastroEndereco viewModel = new FormularioCadastroEndereco { Endereco = obj, Prateleira = prateleiras };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Endereco endereco)
        {
            if (!ModelState.IsValid)
            {
                var prateleiras = await _prateleiraService.FindAllAsync();
                var viewModel = new FormularioCadastroEndereco { Prateleira = prateleiras, Endereco = endereco };
            }
            if (id != endereco.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Os Id fornecido nao correspondem" });
            }
            try
            {
                await _enderecoService.UpdateAsync(endereco);
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
