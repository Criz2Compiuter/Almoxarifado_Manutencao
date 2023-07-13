using Microsoft.AspNetCore.Identity;
namespace Api_Almoxarifado_Mirvi.Models
{
    public class Usuario : IdentityUser
    {
        public string Nome { get; set; }
        public Usuario() : base()
        {
        }
    }
}
