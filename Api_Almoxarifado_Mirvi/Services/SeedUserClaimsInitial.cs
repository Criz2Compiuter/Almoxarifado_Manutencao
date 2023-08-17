using Microsoft.AspNetCore.Identity;

namespace Api_Almoxarifado_Mirvi.Services
{
    public class SeedUserClaimsInitial : ISeedUserClaimsInitial
    {
        private readonly UserManager<IdentityUser> _userManager;

        public SeedUserClaimsInitial(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task SeedUserClaims()
        {
            try
            {
                IdentityUser user1 = await _userManager.FindByNameAsync("Admin");
                if(user1 is not null)
                {
                    var claimsList = (await _userManager.GetClaimsAsync(user1)).Select(p => p.Type);

                    if (!claimsList.Contains("CadastradoEm"))
                    {
                        var claimResult1 = await _userManager.AddClaimAsync(user1, new System.Security.Claims.Claim("CadastradoEm", DateTime.Now.ToString()));
                    }
                    if (!claimsList.Contains("IsAdmin"))
                    {
                        var claimListResult2 = await _userManager.AddClaimAsync(user1, new System.Security.Claims.Claim("IsAdmin", "true"));
                    }
                }

                IdentityUser user2 = await _userManager.FindByNameAsync("User");
                if (user2 is not null)
                {
                    var claimsList = (await _userManager.GetClaimsAsync(user2)).Select(p => p.Type);

                    if (!claimsList.Contains("IsAdmin"))
                    {
                        var claimResult1 = await _userManager.AddClaimAsync(user2, new System.Security.Claims.Claim("IsAdmin", "false"));
                    }
                    if (!claimsList.Contains("IsAdmin"))
                    {
                        var claimListresult2 = await _userManager.AddClaimAsync(user2, new System.Security.Claims.Claim("IsFuncionario", "true"));
                    }
                }

                IdentityUser user3 = await _userManager.FindByNameAsync("Mecanico");
                if (user3 is not null)
                {
                    var claimsList = (await _userManager.GetClaimsAsync(user3)).Select(p => p.Type);

                    if (!claimsList.Contains("IsAdmin"))
                    {
                        var claimResult1 = await _userManager.AddClaimAsync(user2, new System.Security.Claims.Claim("IsAdmin", "false"));
                    }
                    if (!claimsList.Contains("IsAdmin"))
                    {
                        var claimListresult2 = await _userManager.AddClaimAsync(user2, new System.Security.Claims.Claim("IsMecanico", "true"));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
