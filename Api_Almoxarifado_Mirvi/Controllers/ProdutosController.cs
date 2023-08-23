using Api_Almoxarifado_Mirvi.Models;
using Api_Almoxarifado_Mirvi.Models.ViewModels;
using Api_Almoxarifado_Mirvi.Services;
using Api_Almoxarifado_Mirvi.Services.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

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
        private readonly UserManager<IdentityUser> _usuario;

        public ProdutosController(ProdutosService produtoService, PrateleiraService prateleiraService, AlmoxarifadoService almoxarifadoService,
            CorredorService corredorService, RepartiçõesService repartiçõesService, MaquinasService maquinasService, UserManager<IdentityUser> usuario)
        {
            _produtoService = produtoService;
            _prateleiraService = prateleiraService;
            _almoxarifadoService = almoxarifadoService;
            _corredorService = corredorService;
            _repartiçõesService = repartiçõesService;
            _maquinasService = maquinasService;
            this._usuario = usuario;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int almoxarifadoId, int? reparticaoId, int? maquinaId)
        {
            ViewBag.AlmoxarifadoId = almoxarifadoId;
            var produtos = await _produtoService.FindAllInAlmoxarifadoAsync(almoxarifadoId);
            if (reparticaoId != null)
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

        [Authorize(Policy = "IsAdminClaimAccess")]
        public async Task<IActionResult> Create(int almoxarifadoId)
        {
            ViewBag.AlmoxarifadoId = almoxarifadoId;
            var almoxarifado = await _almoxarifadoService.FindAllAsync();
            var prateleiras = await _prateleiraService.FindAllAsync();
            var maquina = await _maquinasService.FindAllAsync();
            var repartição = await _repartiçõesService.FindAllAsync();
            var viewModel = new FormularioCadastroProduto { Almoxarifado = almoxarifado, Prateleira = prateleiras, Maquina = maquina, Repartição = repartição, IdAlmoxarifado = almoxarifadoId };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "IsAdminClaimAccess")]
        public async Task<IActionResult> Create(FormularioCadastroProduto inputViewModel, int almoxarifadoId)
        {
            var produto = inputViewModel.Produto;

            if (inputViewModel.ImagemProduto != null && inputViewModel.ImagemProduto.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    inputViewModel.ImagemProduto.CopyTo(ms);
                    produto.FotoPDF = ms.ToArray();
                }
            }
            else
            {
                produto.FotoPDF = new byte[0];
            }

            if (!ModelState.IsValid)
            {
                await _produtoService.InsertAsync(produto);
                return RedirectToAction(nameof(Index), new { almoxarifadoId });
            }

            var corredores = await _corredorService.FindAllAsync();
            var almoxarifado = await _almoxarifadoService.FindAllAsync();
            var prateleira = await _prateleiraService.FindAllAsync();
            var maquina = await _maquinasService.FindAllAsync();
            var reparticao = await _repartiçõesService.FindAllAsync();
            var viewModel = new FormularioCadastroProduto
            {
                Almoxarifado = almoxarifado,
                Corredor = corredores,
                Prateleira = prateleira,
                Maquina = maquina,
                Repartição = reparticao,
                Produto = produto
            };
            return View(viewModel);
        }

        [Authorize(Policy = "IsMecanicoClaimAccess")]
        [Authorize(Policy = "IsAdminClaimAccess")]
        [Authorize(Policy = "IsFuncionarioClaimAccess")]
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
        [Authorize(Policy = "IsAdminClaimAccess")]
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

        [HttpGet]
        [Authorize(Policy = "IsAdminClaimAccess")]
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

            if (obj.FotoPDF != null)
            {
                ViewBag.FotoPDF = Convert.ToBase64String(obj.FotoPDF);
            }
            else
            {
                ViewBag.FotoPDF = null;
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "IsAdminClaimAccess")]
        public async Task<IActionResult> Edit(int id, FormularioCadastroProduto inputViewModel, int almoxarifadoId)
        {
            if (id != inputViewModel.Produto.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Os Id fornecido nao correspondem" });
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    var produto = await _produtoService.FindByIdAsync(id);

                    if (produto == null)
                    {
                        return RedirectToAction(nameof(Error), new { message = "Produto não encontrado" });
                    }
                    produto.Descricao = inputViewModel.Produto.Descricao;
                    produto.Endereco = inputViewModel.Produto.Endereco;
                    if (inputViewModel.ImagemProduto != null && inputViewModel.ImagemProduto.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            inputViewModel.ImagemProduto.CopyTo(ms);
                            produto.FotoPDF = ms.ToArray();
                        }
                    }
                    await _produtoService.UpdateAsync(produto);
                    return RedirectToAction("Index", new { almoxarifadoId });
                }
                catch (ApplicationException e)
                {
                    return RedirectToAction(nameof(Error), new { message = e.Message });
                }
            }
            inputViewModel.Almoxarifado = await _almoxarifadoService.FindAllAsync();
            inputViewModel.Prateleira = await _prateleiraService.FindAllAsync();
            inputViewModel.Maquina = await _maquinasService.FindAllAsync();
            inputViewModel.Repartição = await _repartiçõesService.FindAllAsync();
            return View(inputViewModel);
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
        [Authorize(Policy = "IsAdminClaimAccess")]
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
                if (produtoindisponivelId == 1)
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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ProdutosIndisponiveis(int minimo, int maximo, int almoxarifadoId)
        {
            ViewBag.AlmoxarifadoId = almoxarifadoId;
            var unavailableProducts = await _produtoService.ObterProdutosIndisponíveisAsync(minimo, maximo, almoxarifadoId);
            return View(unavailableProducts);
        }

        [HttpGet]
        [Authorize(Policy = "IsMecanicoClaimAccess")]
        [Authorize(Policy = "IsAdminClaimAccess")]
        public async Task<IActionResult> Search(int almoxarifadoId, string searchValue)
        {
            var products = await _produtoService.SearchByAlmoxarifadoAsync(almoxarifadoId, searchValue);
            return PartialView("_ProductListPartial", products);
        }

        [HttpGet]
        [Authorize(Policy = "IsMecanicoClaimAccess")]
        [Authorize(Policy = "IsAdminClaimAccess")]
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
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "IsMecanicoClaimAccess")]
        [Authorize(Policy = "IsAdminClaimAccess")]
        public async Task<IActionResult> DescontarQuantidade(int id, int quantidade)
        {
            try
            {
                var produto = await _produtoService.FindByIdAsync(id);
                if (produto == null)
                {
                    return NotFound();
                }
                else if (quantidade == 0 || quantidade < 0)
                {
                    return RedirectToAction(nameof(Error), new { message = "Descontado um valor igual ou menor a zero " });
                }

                var nomeUsuario = Request.Cookies["NomeUsuario"];
                try
                {
                    await _produtoService.DescontarQuantidadeAsync(id, quantidade, nomeUsuario);
                    return RedirectToAction("Index", "Home");
                }
                catch (IntegreityException ex)
                {
                    return RedirectToAction(nameof(Error), new { message = ex.Message });
                }
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize(Policy = "IsMecanicoClaimAccess")]
        [Authorize(Policy = "IsAdminClaimAccess")]
        public async Task<IActionResult> Historico()
        {
            var nomeUsuario = Request.Cookies["NomeUsuario"];

            await _produtoService.CleanUpHistorico();

            var historicos = await _produtoService.GetHistoricoByNomeUsuario(nomeUsuario);

            var viewModel = new HistoricoDescontosViewModel
            {
                NomeUsuario = nomeUsuario,
                HistoricoDescontos = historicos
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SalvarNome(string nome)
        {
            if (!string.IsNullOrWhiteSpace(nome))
            {
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(7)
                };
                Response.Cookies.Append("NomeUsuario", nome, cookieOptions);
            }

            return RedirectToAction("Historico", "Produtos");
        }
    }
}