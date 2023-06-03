using Api_Almoxarifado_Mirvi.Models;
using Api_Almoxarifado_Mirvi.Models.ViewModels;
using Api_Almoxarifado_Mirvi.Services;
using Api_Almoxarifado_Mirvi.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Api_Almoxarifado_Mirvi.Controllers
{
    public class ProdutosController : Controller
    {

        private readonly ProdutosService _produtoService;
        private readonly PrateleiraService _prateleiraService;
        private readonly EnderecoService _enderecoService;
        public ProdutosController(ProdutosService produtoService, PrateleiraService prateleiraService, EnderecoService enderecoService)
        {
            _produtoService = produtoService;
            _prateleiraService = prateleiraService;
            _enderecoService = enderecoService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _produtoService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            if (!ModelState.IsValid)
            {

            }
            var enderecos = await _enderecoService.FindAllAsync();
            var prateleiras = await _prateleiraService.FindAllAsync();
            var viewModel = new FormularioCadastroProduto { Prateleira = prateleiras, Endereco = enderecos };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produto produto)
        {
            await _produtoService.InsertAsync(produto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nao foi fornecido" });
            }

            var obj = await _produtoService.FindByIdAsync(id.Value);
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
                await _produtoService.RemoveAsync(id);
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

            var obj = await _produtoService.FindByIdAsync(id.Value);
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

            var obj = await _produtoService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nao encontrado" });
            }

            List<Prateleira> prateleiras = await _prateleiraService.FindAllAsync();
            List<Endereco>? enderecos = await _enderecoService.FindAllAsync();
            FormularioCadastroProduto viewModel = new FormularioCadastroProduto { Produto = obj, Prateleira = prateleiras, Endereco = enderecos };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Produto produto)
        {
            if (!ModelState.IsValid)
            {
                var prateleiras = await _prateleiraService.FindAllAsync();
                var enderecos = await _enderecoService.FindAllAsync();
                var viewModel = new FormularioCadastroProduto { Prateleira = prateleiras, Endereco = enderecos, Produto = produto };
                return View(viewModel);
            }
            if (id != produto.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Os Id fornecido nao correspondem" });
            }
            try
            {
                await _produtoService.UpdateAsync(produto);
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
