using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api_Almoxarifado_Mirvi.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles ="Admin")]
public class AdminUsersController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    public AdminUsersController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var users = _userManager.Users;
        return View(users);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if(user == null)
        {
            ViewBag.ErrorMessage = $"Usuario com Id = {id} nao foi encontrado";
            return View("NotFound");
        }
        else
        {
            var result = await _userManager.DeleteAsync(user);

            if(result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View("Index");
        }
    }
}
