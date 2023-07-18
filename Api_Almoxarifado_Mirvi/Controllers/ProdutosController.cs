using Api_Almoxarifado_Mirvi.Models;
using Api_Almoxarifado_Mirvi.Models.ViewModels;
using Api_Almoxarifado_Mirvi.Services;
using Api_Almoxarifado_Mirvi.Services.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Api_Almoxarifado_Mirvi.Controllers
{
    [Authorize]
    public class ProdutosController : Controller
    {

        private readonly ProdutosService _produtoService;
        private readonly CorredorService _corredorService;
        private readonly PrateleiraService _prateleiraService;
        private readonly AlmoxarifadoService _almoxarifadoService;
        private readonly RepartiçõesService _repartiçõesService;
        private readonly MaquinasService _maquinasService;

        public ProdutosController(ProdutosService produtoService, PrateleiraService prateleiraService, AlmoxarifadoService almoxarifadoService, CorredorService corredorService, RepartiçõesService repartiçõesService, MaquinasService maquinasService)
        {
            _produtoService = produtoService;
            _prateleiraService = prateleiraService;
            _almoxarifadoService = almoxarifadoService;
            _corredorService = corredorService;
            _repartiçõesService = repartiçõesService;
            _maquinasService = maquinasService;
        }

        public async Task<IActionResult> Index(int almoxarifadoId, int? reparticaoId, int? maquinaId)
        {
            ViewBag.AlmoxarifadoId = almoxarifadoId;
            var produtos = await _produtoService.FindAllInAlmoxarifadoAsync(almoxarifadoId);
            if(reparticaoId != null)
            {
                var produtosReparticao = await _produtoService.FindAllInReparticaoAsync(reparticaoId);
                return View(produtosReparticao);
            }
            else if (maquinaId != null)
            {
                var produtosMaquinas = await _produtoService.FindAllInMaquinaAsync(maquinaId);
                return View(produtosMaquinas);
            }
            return View(produtos);
        }

        public async Task<IActionResult> Create(int almoxarifadoId)
        {
            ViewBag.AlmoxarifadoId = almoxarifadoId;
            var almoxarifado = await _almoxarifadoService.FindAllAsync();
            var prateleiras = await _prateleiraService.FindAllAsync();
            var maquina = await _maquinasService.FindAllAsync();
            var repartição = await _repartiçõesService.FindAllAsync();
            var viewModel = new FormularioCadastroProduto { Almoxarifado = almoxarifado, Prateleira = prateleiras, Maquina = maquina, Repartição = repartição, IdAlmoxarifado = almoxarifadoId};
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produto produto, int almoxarifadoId)
        {
            await _produtoService.InsertAsync(produto);
            return RedirectToAction(nameof(Index), new { almoxarifadoId });
        }

        public async Task<IActionResult> Delete(int? id, int almoxarifadoId)
        {
            ViewBag.AlmoxarifadoId = almoxarifadoId;
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
        public async Task<IActionResult> Delete(int id, int almoxarifadoId)
        {
            try
            {
                await _produtoService.RemoveAsync(id);
                return RedirectToAction("Index", new { almoxarifadoId });
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

            var obj = await _produtoService.FindByIdAsync(id.Value);
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

            var obj = await _produtoService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id nao encontrado" });
            }

            List<Almoxarifado> almoxarifado = await _almoxarifadoService.FindAllAsync();
            List<Corredor> corredor = await _corredorService.FindAllAsync();
            List<Prateleira> prateleiras = await _prateleiraService.FindAllAsync();
            List<Maquina> maquina = await _maquinasService.FindAllAsync();
            List<Repartição> repartição = await _repartiçõesService.FindAllAsync();
            FormularioCadastroProduto viewModel = new FormularioCadastroProduto
            {
                Produto = obj,
                Almoxarifado = almoxarifado,
                Corredor = corredor,
                Prateleira = prateleiras,
                Maquina = maquina,
                Repartição = repartição,
                IdAlmoxarifado = almoxarifadoId
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Produto produto, int almoxarifadoId)
        {
            if (ModelState.IsValid)
            {
                var almoxarifado = await _almoxarifadoService.FindAllAsync();
                var corredor = await _corredorService.FindAllAsync();
                var prateleiras = await _prateleiraService.FindAllAsync();
                var maquina = await _maquinasService.FindAllAsync();
                var repartição = await _repartiçõesService.FindAllAsync();
                var viewModel = new FormularioCadastroProduto
                {
                    Produto = produto,
                    Almoxarifado = almoxarifado,
                    Corredor = corredor,
                    Prateleira = prateleiras,
                    Maquina = maquina,
                    Repartição = repartição,
                    IdAlmoxarifado = almoxarifadoId
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
                return RedirectToAction("Index", new { almoxarifadoId });
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
        public async Task<IActionResult> Atualizar(int id, int quantidade, int almoxarifadoId, int produtoindisponivelId)
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

                await _produtoService.UpdateAsync(produto);
                if(produtoindisponivelId == 1)
                {
                    return RedirectToAction("Produtosindisponiveis", new { almoxarifadoId });
                }
                return RedirectToAction("Index", new { almoxarifadoId });
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> ProdutosIndisponiveis(int minimo, int maximo, int almoxarifadoId)
        {
            ViewBag.AlmoxarifadoId = almoxarifadoId;
            var unavailableProducts = await _produtoService.ObterProdutosIndisponíveisAsync(minimo, maximo, almoxarifadoId);
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

        [HttpPost]
        public async Task<IActionResult> DescontarQuantidade(int productId, int quantidade)
        {
            try
            {
                await _produtoService.DeduzirQuantidadeAsync(productId, quantidade);
                return RedirectToAction("Index", "Home");
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
