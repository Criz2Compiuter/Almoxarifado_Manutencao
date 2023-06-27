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
        private readonly CorredorService _corredorService;
        private readonly PrateleiraService _prateleiraService;
        private readonly EnderecoService _enderecoService;
        private readonly AlmoxarifadoService _almoxarifadoService;
        private readonly RepartiçõesService _repartiçõesService;
        private readonly MaquinasService _maquinasService;

        public ProdutosController(ProdutosService produtoService, PrateleiraService prateleiraService,
        EnderecoService enderecoService, AlmoxarifadoService almoxarifadoService, CorredorService corredorService, RepartiçõesService repartiçõesService, MaquinasService maquinasService)
        {
            _produtoService = produtoService;
            _prateleiraService = prateleiraService;
            _enderecoService = enderecoService;
            _almoxarifadoService = almoxarifadoService;
            _corredorService = corredorService;
            _repartiçõesService = repartiçõesService;
            _maquinasService = maquinasService;
        }

        public async Task<IActionResult> Index(int almoxarifadoId)
        {
            var produtos = await _produtoService.FindAllInAlmoxarifadoAsync(almoxarifadoId);
            return View(produtos);
        }

        public async Task<IActionResult> Create(int almoxarifadoId)
        {
            ViewData["AlmoxarifadoId"] = almoxarifadoId;
            var enderecos = await _enderecoService.FindAllAsync();
            var prateleiras = await _prateleiraService.FindAllAsync();
            var maquina = await _maquinasService.FindAllAsync();
            var repatição = await _repartiçõesService.FindAllAsync();
            var viewModel = new FormularioCadastroProduto { Prateleira = prateleiras, Endereco = enderecos, Maquina = maquina, Repartição = repatição };
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
            List<Maquina> maquina = await _maquinasService.FindAllAsync();
            List<Repartição> repartição = await _repartiçõesService.FindAllAsync();
            
            FormularioCadastroProduto viewModel = new FormularioCadastroProduto
            {
                Prateleira = prateleiras,
                Endereco = enderecos,
                Maquina = maquina,
                Repartição = repartição
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Produto produto)
        {
            if (ModelState.IsValid)
            {
                var prateleiras = await _prateleiraService.FindAllAsync();
                var enderecos = await _enderecoService.FindAllAsync();
                var maquina = await _maquinasService.FindAllAsync();
                var repartição = await _repartiçõesService.FindAllAsync();
                var viewModel = new FormularioCadastroProduto
                {
                    Prateleira = prateleiras,
                    Endereco = enderecos,
                    Maquina = maquina,
                    Repartição = repartição
                };
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizar(int id, int quantidade, int minimo, int maximo)
        {
            try
            {
                var produto = await _produtoService.FindByIdAsync(id);
                if (produto == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Produto não encontrado" });
                }

                produto.Quantidade = quantidade;
                produto.AtualizarStatus();

                await _produtoService.AtualizarProduto(produto);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> ProdutosIndisponiveis(int minimo, int maximo)
        {
            var unavailableProducts = await _produtoService.ObterProdutosIndisponíveisAsync(minimo, maximo);
            return View(unavailableProducts);
        }

        [HttpGet]
        public async Task<IActionResult> Search(int almoxarifadoId, string searchValue)
        {
            var products = await _produtoService.SearchByAlmoxarifadoAsync(almoxarifadoId, searchValue);
            return PartialView("_ProductListPartial", products);
        }

        [HttpGet]
        public async Task<IActionResult> SearchByAlmoxarifado(int almoxarifadoId, string searchValue)
        {
            var products = await _produtoService.FindByAlmoxarifadoAsync(almoxarifadoId);

            if (!string.IsNullOrEmpty(searchValue))
            {
                products = products.Where(p => p.Descricao.Contains(searchValue)).ToList();
            }

            return PartialView("_ProductListPartial", products);
        }
    }
}
