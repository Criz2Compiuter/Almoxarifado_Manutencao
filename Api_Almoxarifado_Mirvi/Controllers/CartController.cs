using Api_Almoxarifado_Mirvi.Models;
using Api_Almoxarifado_Mirvi.Models.ViewModels;
using Api_Almoxarifado_Mirvi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api_Almoxarifado_Mirvi.Controllers
{
    public class CartController : Controller
    {
        private readonly ProdutosService _produtosService;
        private readonly CartBuy _carrinhoCompra;

        public CartController(ProdutosService produtosService, CartBuy carrinhoCompra)
        {
            _produtosService = produtosService;
            _carrinhoCompra = carrinhoCompra;
        }

        public IActionResult Index()
        {
            var itens = _carrinhoCompra.GetCarrinhoItens();
            _carrinhoCompra.CarrinhoCompraItens = itens;

            var carrinhoCompraViewModel = new CarrinhoCompraViewModel
            {
                CartBuy = _carrinhoCompra,
                carrinhoCompraTotal = _carrinhoCompra.GetcarrinhoCompraTotal()
            };

            return View(carrinhoCompraViewModel);
        }

        public async Task<RedirectToActionResult> AdcionarItemNoCarrinhoCompra(int id)
        {
            var produtoSelecionado = await _produtosService.FindByIdAsync(id);

            if (produtoSelecionado != null)
            {
                _carrinhoCompra.AdicionarAoCarrinho(produtoSelecionado);
            }
            
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoverItemDoCarrinho(int id)
        {
            var produtoSelecionado = await _produtosService.FindByIdAsync(id);
            if(produtoSelecionado != null)
            {
                _carrinhoCompra.RemoveDoCarrinho(produtoSelecionado);
            }
            return RedirectToAction("Index");
        }
    }
}
