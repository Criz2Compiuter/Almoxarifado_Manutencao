using Api_Almoxarifado_Mirvi.Models.ViewModels;
using Api_Almoxarifado_Mirvi.Services;
using Api_Almoxarifado_Mirvi.Services.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            return View(produtos);
        }

        [Authorize(Policy = "RequireUserAdminMecanicoRole")]
        public async Task<IActionResult> MirviBrasil()
        {
            ViewData["ActiveTab"] = "MirviBrasil";
                return View();
        }

        [Authorize(Policy = "RequireUserAdminMecanicoRole")]
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

        [Authorize(Policy = "RequireUserAdminMecanicoRole")]
        [HttpGet]
        public async Task<IActionResult> Search(string searchValue)
        {
            var products = await _produtosService.FindByDescriptionAsync(searchValue);
            return PartialView("_ProductListPartial", products);
        }

        [Authorize(Policy = "RequireUserAdminMecanicoRole")]
        [HttpPost]
        public async Task<IActionResult> DescontarQuantidade(int id, int quantidade)
        {
            try
            {
                await _produtosService.DeduzirQuantidadeAsync(id, quantidade);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}