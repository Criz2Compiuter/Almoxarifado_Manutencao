using Microsoft.AspNetCore.Identity;

namespace Api_Almoxarifado_Mirvi.Services
{
    public class SeedUserRolesInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRolesInitial(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedRolesAsync()
        {
            if (!await _roleManager.RoleExistsAsync("User"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "User";
                role.NormalizedName = "USER";
                role.ConcurrencyStamp = Guid.NewGuid().ToString();

                IdentityResult roleResult = await _roleManager.CreateAsync(role);
            }

            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                role.ConcurrencyStamp = Guid.NewGuid().ToString();
                IdentityResult roleResult = await _roleManager.CreateAsync(role);
            }

            if (!await _roleManager.RoleExistsAsync("Mecanico"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Mecanico";
                role.NormalizedName = "MECANICO";
                role.ConcurrencyStamp = Guid.NewGuid().ToString();
                IdentityResult roleResult = await _roleManager.CreateAsync(role);
            }

        }

        public async Task SeedUsersAsync()
        {
            if (await _userManager.FindByNameAsync("usuario") == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "usuario";
                user.NormalizedUserName = "USUARIO";
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = await _userManager.CreateAsync(user, "Cris1234567.");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }
            }

            if (await _userManager.FindByNameAsync("Admin") == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "Admin";
                user.NormalizedUserName = "ADMIN";
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = await _userManager.CreateAsync(user, "Cris1234567.");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
            }

            if (await _userManager.FindByNameAsync("Mecanico") == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "Mecanico";
                user.NormalizedUserName = "MECANICO";
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = await _userManager.CreateAsync(user, "Cris1234567.");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Mecanico");
                }
            }
        }
    }
}
