using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VShop.ProductApi.Services.Interfaces;

namespace VShop.ProductApi.Controllers;

[Route("api/categories")]
[ApiController]
public class CategoriesController(ICategoryService _categoryService) : ControllerBase
{

}
