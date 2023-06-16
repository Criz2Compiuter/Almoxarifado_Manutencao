using Api_Almoxarifado_Mirvi.Models.ViewModels;
using Api_Almoxarifado_Mirvi.Services;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;
using System.Diagnostics;

namespace Api_Almoxarifado_Mirvi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProdutosService _produtosService;

        public HomeController(ILogger<HomeController> logger, ProdutosService produtosService)
        {
            _logger = logger;
            _produtosService = produtosService;
        }

        public async Task<IActionResult> Index()
        {
            var produtos = await _produtosService.FindAllAsync();
            ViewData["ActiveTab"] = "Index";
            return View(produtos);
        }
        public IActionResult MirviBrasil()
        {
            ViewData["ActiveTab"] = "MirviBrasil";
            return View();
        }
        public IActionResult TetraPak()
        {
            ViewData["ActiveTab"] = "TetraPak";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchValue)
        {
            var products = await _produtosService.FindByDescriptionAsync(searchValue);
            return PartialView("_ProductListPartial", products);
        }
    }
}