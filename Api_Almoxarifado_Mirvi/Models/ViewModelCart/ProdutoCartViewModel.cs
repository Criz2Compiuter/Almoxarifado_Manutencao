using System.ComponentModel.DataAnnotations;

namespace Api_Almoxarifado_Mirvi.Models.ViewModelCart
{
    public class ProdutoCartViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        [Range(0, 100000)]
        public decimal Valor { get; set; }
        [Display(Name = "Imagem do Produto")]
        public IFormFile ImagemProduto { get; set; }
        [Required]
        public string CodigoDeCompra { get; set; }
        [Required]
        public int Quantidade { get; set; }
    }
}
