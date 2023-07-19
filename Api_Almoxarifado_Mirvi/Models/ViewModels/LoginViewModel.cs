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
        [Display(Name = "Lembrar-me")]
        public bool Remember { get; set; }
    }
}
