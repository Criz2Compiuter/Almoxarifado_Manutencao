using Api_Almoxarifado_Mirvi.Models;
using Api_Almoxarifado_Mirvi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Api_Almoxarifado_Mirvi.Components
{
    public class CarrinhoCompraResumo : ViewComponent
    {
        private CartBuy _carrinhoCompra;

        public CarrinhoCompraResumo(CartBuy cartBuy)
        {
            _carrinhoCompra = cartBuy;
        }

        public IViewComponentResult Invoke()
        {
            //var itens = _carrinhoCompra.GetCarrinhoItens();

            var itens = new List<CartBuyItem>() { new CartBuyItem(), new CartBuyItem() };

            _carrinhoCompra.CarrinhoCompraItens = itens;

            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                CartBuy = _carrinhoCompra,
                carrinhoCompraTotal = _carrinhoCompra.GetcarrinhoCompraTotal()
            };
            return View(carrinhoCompraVM);
        }
    }
}
