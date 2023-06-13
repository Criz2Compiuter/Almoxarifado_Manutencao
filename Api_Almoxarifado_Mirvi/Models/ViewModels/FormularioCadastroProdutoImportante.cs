namespace Api_Almoxarifado_Mirvi.Models.ViewModels
{
    public class FormularioCadastroProdutoImportante
    {
        public ProdutoImportante ProdutoImportante { get; set; }
        public ICollection<Endereco>? Endereco { get; set; }
        public ICollection<Prateleira> Prateleira { get; set; }
    }
}