using System.ComponentModel.DataAnnotations;

namespace Api_Almoxarifado_Mirvi.Data.Dtos
{
    public class CreateDto : IValidatableObject
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Cargo { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string SenhaParaCadastro { get; set; }

        [Compare("SenhaParaCadastro")]
        public string ConfirmacaoSenhaParaCadastro { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (SenhaParaCadastro != ConfirmacaoSenhaParaCadastro)
            {
                yield return new ValidationResult("A confirmação da senha para cadastro não corresponde à senha informada.", new[] { nameof(ConfirmacaoSenhaParaCadastro) });
            }
        }
    }
}
