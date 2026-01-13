using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VShop.Web.Models;
using VShop.Web.Roles;
using VShop.Web.Services.Interfaces;

namespace VShop.Web.Controllers;

public class ProductController(IProductService produtoService,ICategoryService categoryService) : Controller
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()  
    {
        var result = await produtoService.GetAllProducts("");

        if (result is null)
            return View("Error");

        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {
        ViewBag.CategoryId = new SelectList(await categoryService.GetAllCategories(""), "Id", "Name");
        return View();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateProduct(ProductViewModel productVM)
    {

        if(ModelState.IsValid)
        {
            var result = await produtoService.CreateProduct(productVM, "");

            if(result is not null)
                return RedirectToAction(nameof(Index));
        }

        ViewBag.CategoryId = new SelectList(await categoryService.GetAllCategories(""), "Id", "Name");
        return View(productVM);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateProduct(int id)
    {

        ViewBag.CategoryId = new SelectList(await categoryService.GetAllCategories(""), "Id", "Name");

        var result = await produtoService.FindProductById(id, "");

        if(result is null)
            return View("Error");

        return View(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> UpdateProduct(ProductViewModel productVM)
    {
        if (ModelState.IsValid)
        {
            var result = produtoService.UpdateProduct(productVM, "");

            if (result is not null)
                return RedirectToAction(nameof(Index));
        }

        ViewBag.CategoryId = new SelectList(await categoryService.GetAllCategories(""), "Id", "Name");

        return View(productVM);
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ProductViewModel>> DeleteProduct(int id)
    {
        var result = await produtoService.FindProductById(id, "");

        if(result is null)
            return View("Error");

        return View(result);
    }

    [Authorize(Roles = Role.Admin)]
    [HttpPost(), ActionName("DeleteProduct")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await produtoService.DeleteProductById(id, "");

        if(!result)
            return View("Error");

        return RedirectToAction(nameof(Index));
    }
}
