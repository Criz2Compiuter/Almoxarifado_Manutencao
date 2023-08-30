using System.Data.Entity;
using System.Drawing;

namespace Api_Almoxarifado_Mirvi.Models;

public class CarrinhoCompra
{
    private readonly Api_Almoxarifado_MirviContext _context;
    public CarrinhoCompra(Api_Almoxarifado_MirviContext context)
    {
        _context = context;
    }
    public string CarrinhoCompraId { get; set; }
    public List<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }

    public static CarrinhoCompra GetCart(IServiceProvider services)
    {
        var httpContextAccessor = services.GetRequiredService<IHttpContextAccessor>();
        var context = services.GetRequiredService<Api_Almoxarifado_MirviContext>();

        ISession session = httpContextAccessor.HttpContext.Session;

        string cartBuyId = session.GetString("CartBuyId") ?? Guid.NewGuid().ToString();
        session.SetString("CartBuyId", cartBuyId);

        return new CarrinhoCompra(context)
        {
            CarrinhoCompraId = cartBuyId
        };
    }

    public void AdicionarAoCarrinho(Produto produto, int quantidade)
    {
        var carrinhoCompraItem = _context.CarrinhoCompraItem.SingleOrDefault(
            s => s.Produto.Id == produto.Id && s.CarrinhoCompraItemId == CarrinhoCompraId);

        if (carrinhoCompraItem == null)
        {
            carrinhoCompraItem = new CarrinhoCompraItem
            {
                CarrinhoCompraId = CarrinhoCompraId,
                Produto = produto,
                Quantidade = quantidade
            };
            _context.CarrinhoCompraItem.Add(carrinhoCompraItem);
        }
        else
        {
            carrinhoCompraItem.Quantidade++;
        }
        _context.SaveChanges();
    }

    public int RemoveDoCarrinho(Produto produto)
    {
        var carrinhoCompraItem = _context.CarrinhoCompraItem.SingleOrDefault(
            s => s.Produto.Id == produto.Id && s.CarrinhoCompraId == CarrinhoCompraId);

        var quantidadeLocal = 0;

        if (carrinhoCompraItem != null)
        {
            if (carrinhoCompraItem.Quantidade > 1)
            {
                carrinhoCompraItem.Quantidade--;
                quantidadeLocal = carrinhoCompraItem.Quantidade;
            }
            else
            {
                _context.CarrinhoCompraItem.Remove(carrinhoCompraItem);
            }
        }

        _context.SaveChanges();

        return quantidadeLocal;
    }

    public List<CarrinhoCompraItem> GetCarrinhoItens()
    {
        return CarrinhoCompraItens ??
            (CarrinhoCompraItens =
            _context.CarrinhoCompraItem.Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
            .Include(s => s.Produto)
            .ToList());
    }

    public void LimparCarrinho()
    {
        var carrinhoItens = _context.CarrinhoCompraItem
            .Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);

        _context.CarrinhoCompraItem.RemoveRange(carrinhoItens);

        _context.SaveChanges();
    }

    public decimal GetCarrinhoCompraTotal()
    {
        var total = _context.CarrinhoCompraItem.Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
            .Select(c => c.Produto.Valor * c.Quantidade).Sum();

        return total;
    }
}