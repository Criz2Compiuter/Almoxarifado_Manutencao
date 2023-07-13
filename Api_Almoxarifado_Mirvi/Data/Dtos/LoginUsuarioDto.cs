using System.ComponentModel.DataAnnotations;

namespace Api_Almoxarifado_Mirvi.Data.Dtos
{
    public class LoginUsuarioDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
