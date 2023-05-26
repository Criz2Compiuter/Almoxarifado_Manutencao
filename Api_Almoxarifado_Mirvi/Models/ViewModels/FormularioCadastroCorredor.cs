namespace Api_Almoxarifado_Mirvi.Models.ViewModels
{
    public class FormularioCadastroCorredor
    {
        public Corredor Corredor { get; set; }
        public ICollection<Almoxarifado> Almoxarifados { get; set; }
    }
}
