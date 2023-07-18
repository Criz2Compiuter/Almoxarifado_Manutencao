using System.ComponentModel.DataAnnotations;

namespace Api_Almoxarifado_Mirvi.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string? Nome { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public string SenhaParaRegistrar { get; set; } = "95825776";
        [Required]
        [Display(Name = "Confirmar a senha")]
        [Compare("SenhaParaRegistrar", ErrorMessage = "Senha informada incorreta")]
        public string? SenhaParaVerificarRegistro { get; set; }
        [Display(Name = "Lembrar-me")]
        public bool Remember { get; set; }
    }
}
