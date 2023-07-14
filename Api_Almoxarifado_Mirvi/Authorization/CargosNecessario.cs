using Microsoft.AspNetCore.Authorization;

namespace Api_Almoxarifado_Mirvi.Authorization
{
    public class CargosNecessario : IAuthorizationRequirement
    {
        public CargosNecessario(params string[] nomesCargos)
        {
            NomesCargos = nomesCargos;
        }

        public string[] NomesCargos { get; }
    }
}