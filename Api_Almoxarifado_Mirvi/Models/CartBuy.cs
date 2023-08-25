using Api_Almoxarifado_Mirvi.Migrations;
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
}
