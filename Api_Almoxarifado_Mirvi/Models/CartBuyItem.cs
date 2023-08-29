using System.ComponentModel.DataAnnotations;

namespace Api_Almoxarifado_Mirvi.Models;

public class CartBuyItem
{
    public string CartBuyItemId { get; set; }
    public Produto Produto { get; set; }
    public int Quantidade { get; set; }
    [StringLength(200)]
    public string CarrinhoCompraId { get; set; }
}
