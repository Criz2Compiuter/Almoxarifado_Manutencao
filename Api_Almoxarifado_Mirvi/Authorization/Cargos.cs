using Microsoft.AspNetCore.Authorization;

namespace Api_Almoxarifado_Mirvi.Authorization
{
    public class Cargos : IAuthorizationRequirement
    {
        public Cargos(string visitante, string mecanico, string adm)
        {
            
        }

        public string CargosNome { get; set; }
    }
}
