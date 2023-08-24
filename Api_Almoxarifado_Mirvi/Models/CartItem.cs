using System.ComponentModel.DataAnnotations;

namespace Api_Almoxarifado_Mirvi.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int Quantidade { get; set; } = 1;
        public DateTime DateCreated { get; set; }
        public int ProdutoId { get; set; }
        public int CartHeaderId { get; set; }
        public Produto Produto { get; set; } = new Produto();
    }
}
