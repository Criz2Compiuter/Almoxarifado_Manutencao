using System.Data.Entity;
using System.Drawing;

namespace Api_Almoxarifado_Mirvi.Models;

public class CartBuy
{
    private readonly Api_Almoxarifado_MirviContext _context;
    public CartBuy(Api_Almoxarifado_MirviContext context)
    {
        _context = context;
    }
    public string CartBuyId { get; set; }
    public List<CartBuyItem> CarrinhoCompraItens { get; set; }

    public static CartBuy GetCart(IServiceProvider services)
    {
        ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

        var context = services.GetService<Api_Almoxarifado_MirviContext>();

        string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

        session.SetString("CarrinhoId", carrinhoId);

        return new CartBuy(context)
        {
            CartBuyId = carrinhoId
        };
    }

    public void AdicionarAoCarrinho(Produto produto)
    {
        var carrinhoCompraItem = _context.CartBuyItems.SingleOrDefault(
            s => s.Produto.Id == produto.Id && s.CartBuyItemId == CartBuyId);

        if(carrinhoCompraItem == null)
        {
            carrinhoCompraItem = new CartBuyItem
            {
                CarrinhoCompraId = CartBuyId,
                Produto = produto,
                Quantidade = 1
            };
            _context.CartBuyItems.Add(carrinhoCompraItem);
        }
        else
        {
            carrinhoCompraItem.Quantidade++;
        }
        _context.SaveChanges();
    }

    public int RemoveDoCarrinho(Produto produto)
    {
        var carrinhoCompraItem = _context.CartBuyItems.SingleOrDefault(
            s => s.Produto.Id == produto.Id && s.CarrinhoCompraId == CartBuyId);

        var quantidadeLocal = 0;

        if(carrinhoCompraItem != null)
        {
            if(carrinhoCompraItem.Quantidade > 1)
            {
                carrinhoCompraItem.Quantidade--;
                quantidadeLocal = carrinhoCompraItem.Quantidade;
            }
            else
            {
                _context.CartBuyItems.Remove(carrinhoCompraItem);
            }
        }

        _context.SaveChanges();

        return quantidadeLocal;
    }

    public List<CartBuyItem> GetCarrinhoItens()
    {
        return CarrinhoCompraItens ??
            (CarrinhoCompraItens =
            _context.CartBuyItems.Where(c => c.CarrinhoCompraId == CartBuyId)
            .Include(s => s.Produto)
            .ToList());
    }

    public void LimparCarrinho()
    {
        var carrinhoItens = _context.CartBuyItems
            .Where(carrinho => carrinho.CarrinhoCompraId == CartBuyId);

        _context.CartBuyItems.RemoveRange(carrinhoItens);

        _context.SaveChanges();
    }

    public decimal GetcarrinhoCompraTotal()
    {
        var total = _context.CartBuyItems.Where(c => c.CarrinhoCompraId == CartBuyId)
            .Select(c => c.Produto.Valor * c.Quantidade).Sum();

        return total;
    }
}
