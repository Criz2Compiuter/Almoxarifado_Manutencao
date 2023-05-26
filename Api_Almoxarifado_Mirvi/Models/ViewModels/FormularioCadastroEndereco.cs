namespace Api_Almoxarifado_Mirvi.Models.ViewModels
{
    public class FormularioCadastroEndereco
    {
        public Endereco Endereco { get; set; }
        public ICollection<Prateleira> Prateleira { get; set; }
    }
}
