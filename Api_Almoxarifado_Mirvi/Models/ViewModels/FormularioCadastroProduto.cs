using System.ComponentModel.DataAnnotations;
namespace Api_Almoxarifado_Mirvi.Models.ViewModels
{
    public class FormularioCadastroProduto
    {
        public Produto Produto { get; set; }
        public ICollection<Almoxarifado> Almoxarifado { get; set; }
        public ICollection<Corredor> Corredor { get; set; }
        public ICollection<Prateleira>? Prateleira { get; set; }
        public ICollection<Maquina>? Maquina { get; set; }
        public ICollection<Repartição>? Repartição { get; set; }
        [Display(Name = "Imagem do Produto")]
        public IFormFile ImagemProduto { get; set; }
        public int IdAlmoxarifado { get; set; }
    }
}