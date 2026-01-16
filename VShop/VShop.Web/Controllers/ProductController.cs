using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VShop.Web.Models;
using VShop.Web.Roles;
using VShop.Web.Services.Interfaces;

namespace VShop.Web.Controllers;


[Authorize(Roles = Role.Admin)]
public class ProductController(IProductService produtoService,ICategoryService categoryService) : Controller
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()  
    {
        var result = await produtoService.GetAllProducts(await GetAccessToken());

        if (result is null)
            return View("Error");

        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {
        ViewBag.CategoryId = new SelectList(await categoryService.GetAllCategories( await GetAccessToken()), "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductViewModel productVM)
    {

        if(ModelState.IsValid)
        {
            var result = await produtoService.CreateProduct(productVM,  await GetAccessToken());

            if(result is not null)
                return RedirectToAction(nameof(Index));
        }

        ViewBag.CategoryId = new SelectList(await categoryService.GetAllCategories( await GetAccessToken()), "Id", "Name");
        return View(productVM);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateProduct(int id)
    {

        ViewBag.CategoryId = new SelectList(await categoryService.GetAllCategories( await GetAccessToken()), "Id", "Name");

        var result = await produtoService.FindProductById(id,  await GetAccessToken());

        if(result is null)
            return View("Error");

        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProduct(ProductViewModel productVM)
    {
        if (ModelState.IsValid)
        {
            var result = produtoService.UpdateProduct(productVM,  await GetAccessToken());

            if (result is not null)
                return RedirectToAction(nameof(Index));
        }

        ViewBag.CategoryId = new SelectList(await categoryService.GetAllCategories( await GetAccessToken()), "Id", "Name");

        return View(productVM);
    }

    [HttpGet]
    public async Task<ActionResult<ProductViewModel>> DeleteProduct(int id)
    {
        var result = await produtoService.FindProductById(id,  await GetAccessToken());

        if(result is null)
            return View("Error");

        return View(result);
    }

    [HttpPost(), ActionName("DeleteProduct")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await produtoService.DeleteProductById(id,  await GetAccessToken());

        if(!result)
            return View("Error");

        return RedirectToAction(nameof(Index));
    }

    private async Task<string> GetAccessToken()
    {
        return await HttpContext.GetTokenAsync("access_token");
    }
}
