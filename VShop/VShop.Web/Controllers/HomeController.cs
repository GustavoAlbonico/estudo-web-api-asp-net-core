using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VShop.Web.Models;
using VShop.Web.Services;
using VShop.Web.Services.Interfaces;

namespace VShop.Web.Controllers
{
    public class HomeController(
        IProductService productService,
        ILogger<HomeController> logger
    ) : Controller
    {


        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAllProducts(string.Empty);

            if (products is null)
            {
                return View("Error");
            }

            return View(products);
        }

        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<ProductViewModel>> ProductDetails(int id)
        {
            //var token = await HttpContext.GetTokenAsync("access_token");
            var product = await productService.FindProductById(id, string.Empty);

            if (product is null)
                return View("Error");

            return View(product);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [Authorize]
        public async Task<IActionResult> Login()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}
