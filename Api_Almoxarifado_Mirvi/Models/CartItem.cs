using System.ComponentModel.DataAnnotations;

namespace Api_Almoxarifado_Mirvi.Models
{
    public class CartItem
    {
        [Key]
        public string Id { get; set; }
        public string CartId { get; set; }
        public int Quantidade { get; set; }
        public DateTime DateCreated { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; } = new Produto();
    }
}
