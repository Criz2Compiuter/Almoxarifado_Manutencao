using Api_Almoxarifado_Mirvi.Models;
using Api_Almoxarifado_Mirvi.Models.Enums;
using Api_Almoxarifado_Mirvi.Models.ViewModels;
using Api_Almoxarifado_Mirvi.Services;
using Api_Almoxarifado_Mirvi.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Api_Almoxarifado_Mirvi.Controllers
{
    public class ProdutosImportantesController : Controller
    {

        private readonly ProdutosImportantesService _produtoImportantesService;
        private readonly PrateleiraService _prateleiraService;
        private readonly EnderecoService _enderecoService;
        public ProdutosImportantesController(ProdutosImportantesService produtoImportantesService, PrateleiraService prateleiraService, EnderecoService enderecoService)
        {
            _produtoImportantesService = produtoImportantesService;
            _prateleiraService = prateleiraService;
            _enderecoService = enderecoService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _produtoImportantesService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var enderecos = await _enderecoService.FindAllAsync();
            var prateleiras = await _prateleiraService.FindAllAsync();
            var viewModel = new FormularioCadastroProdutoImportante { Prateleira = prateleiras, Endereco = enderecos };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutoImportante produtoImportante)
        {
            await _produtoImportantesService.InsertAsync(produtoImportante);
            return RedirectToAction(nameof(Index));
        }
       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nao foi fornecido" });
            }

            var obj = await _produtoImportantesService.FindByIdAsync(id.Value);
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
                await _produtoImportantesService.RemoveAsync(id);
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

            var obj = await _produtoImportantesService.FindByIdAsync(id.Value);
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

            var obj = await _produtoImportantesService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nao encontrado" });
            }

            List<Prateleira> prateleiras = await _prateleiraService.FindAllAsync();
            List<Endereco>? enderecos = await _enderecoService.FindAllAsync();
            FormularioCadastroProdutoImportante viewModel = new FormularioCadastroProdutoImportante { ProdutoImportante = obj, Prateleira = prateleiras, Endereco = enderecos };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProdutoImportante produtoImportante)
        {
            if (ModelState.IsValid)
            {
                var prateleiras = await _prateleiraService.FindAllAsync();
                var enderecos = await _enderecoService.FindAllAsync();
                var viewModel = new FormularioCadastroProdutoImportante { Prateleira = prateleiras, Endereco = enderecos, ProdutoImportante = produtoImportante };
                return View(viewModel);
            }
            if (id != produtoImportante.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Os Id fornecido nao correspondem" });
            }
            try
            {
                await _produtoImportantesService.UpdateAsync(produtoImportante);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizar(int id, int quantidade)
        {
            try
            {
                var produtoImportante = await _produtoImportantesService.FindByIdAsync(id);
                if (produtoImportante == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Produto não encontrado" });
                }

                produtoImportante.Quantidade = quantidade;
                if (quantidade <= 1)
                {
                    produtoImportante.ProdutoStatusImportante = ProdutoStatusImportante.Indisponivelv;
                    produtoImportante.Data = DateTime.Now;
                }
                else if (quantidade > 1 && quantidade < 15)
                {
                    produtoImportante.ProdutoStatusImportante = ProdutoStatusImportante.LimiteBaixov;
                    produtoImportante.Data = DateTime.Now;
                }
                else if (quantidade >= 15)
                {
                    produtoImportante.ProdutoStatusImportante = ProdutoStatusImportante.Disponivelv;
                    produtoImportante.Data = DateTime.Now;
                }
                await _produtoImportantesService.AtualizarProduto(produtoImportante);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> ProdutosIndisponiveis()
        {
            var unavailableProducts = await _produtoImportantesService.ObterProdutosImportantesIndisponíveisAsync();
            return View(unavailableProducts);
        }
    }
}
