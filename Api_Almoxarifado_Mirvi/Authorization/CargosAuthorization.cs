using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api_Almoxarifado_Mirvi.Authorization
{
    public class CargosAuthorization : AuthorizationHandler<CargosNecessario>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CargosNecessario requirement)
        {
            var cargoUsuarioClaim = context.User.FindFirst(claim => claim.Type == ClaimTypes.Role);

            if (cargoUsuarioClaim is null)
                return Task.CompletedTask;

            var cargoUsuario = cargoUsuarioClaim.Value;

            if (requirement.NomesCargos.Contains(cargoUsuario))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
