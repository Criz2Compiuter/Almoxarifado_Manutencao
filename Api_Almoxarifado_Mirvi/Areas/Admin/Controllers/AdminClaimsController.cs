using Api_Almoxarifado_Mirvi.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api_Almoxarifado_Mirvi.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AdminClaimsController : Controller
{
    private UserManager<IdentityUser> _userManager;

    public AdminClaimsController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var users = _userManager.Users;
        return View(users);
    }

    [HttpGet]
    public async Task<IActionResult> EditUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            ModelState.AddModelError("", "Usuario nao encontrado");
            return View();
        }

        var userClaims = await _userManager.GetClaimsAsync(user);

        var model = new EditUserViewModel
        {
            Id = user.Id,
            UserName = user.UserName,
            Claims = userClaims.ToList(),
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditUser(EditUserViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.Id);

        if (user is null)
        {
            ModelState.AddModelError("", "Usuario nao encontrado");
            return View(model);
        }
        else
        {
            user.UserName = model.UserName;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ActionName("Create")]
    public async Task<IActionResult> Create_Post(string claimType, string claimValue, string userId)
    {
        if (claimType is null || claimValue is null)
        {
            ModelState.AddModelError("", "Tipo/valor da Claim deve ser informado");
            return View();
        }
        IdentityUser user = await _userManager.FindByIdAsync(userId);

        if (user is not null)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);

            Claim claim = userClaims.FirstOrDefault(x => x.Type.Equals(claimType)
                            && x.Value.Equals(claimValue));

            if (claim is null)
            {
                Claim newClaim = new Claim(claimType, claimValue);

                IdentityResult result = await _userManager.AddClaimAsync(user, newClaim);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    Errors(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "Claim ja existente");
            }
        }
        else
        {
            ModelState.AddModelError("", "Usuario nao encontrado");
        }
        return View("Index");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteClaim(string claimValues)
    {
        string[] claimValuesArray = claimValues.Split(";");
        string claimType = claimValuesArray[0].ToString();
        string claimValue = claimValuesArray[1].ToString();
        string userId = claimValuesArray[2].ToString();

        IdentityUser user = await _userManager.FindByIdAsync(userId);

        if (user is not null)
        {

            var userClaims = await _userManager.GetClaimsAsync(user);

            Claim claim = userClaims.FirstOrDefault(x => x.Type.Equals(claimType)
                          && x.Value.Equals(claimValue));

            IdentityResult result = await _userManager.RemoveClaimAsync(user, claim);

            if (result.Succeeded)
                return RedirectToAction("Index");
            else
                Errors(result);
        }
        else
        {
            ModelState.AddModelError("", "Usuário não encontrado");
        }

        return View("Index");
    }

    void Errors(IdentityResult result)
    {
        foreach (IdentityError error in result.Errors)
            ModelState.AddModelError("", error.Description);
    }
}