using System.ComponentModel.DataAnnotations;

namespace Api_Almoxarifado_Mirvi.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string? Nome { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar a senha")]
        [Compare("Password", ErrorMessage = "As senhas nao coencidem")]
        public string? ConfirmPassword { get; set; }
        public string SenhaPararegistrar { get; set; } = "95825776";
        [Required]
        [Display(Name = "Confirmar a senha para registro")]
        [Compare("SenhaParaRegistrar", ErrorMessage = "Senha informada incorreta")]
        public string? SenhaParaRegistrar { get; set; }
        
    }
}
