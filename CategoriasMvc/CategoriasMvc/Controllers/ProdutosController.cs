using CategoriasMvc.Models;
using CategoriasMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace CategoriasMvc.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IProdutoService _produtoService;
        private readonly ICategoriaService _categoriaService;
        private string _token = string.Empty;

        public ProdutosController(IProdutoService produtoService, ICategoriaService categoriaService)
        {
            _produtoService = produtoService;
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoViewModel>>> Index()
        {
            var result = await _produtoService.GetProdutos(ObterTokenJwt());

            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> CriarNovoProduto()
        {
            ViewBag.CategoriaId =
                new SelectList(await _categoriaService.GetCategorias(), "Id", "Nome");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoViewModel>> CriarNovoProduto(ProdutoViewModel produtoVM)
        {

            Console.WriteLine(ModelState.IsValid);
            Console.WriteLine(produtoVM.ToString());

            if (ModelState.IsValid)
            {
                var result = await _produtoService.CriarProduto(produtoVM, ObterTokenJwt());

                if (result != null)
                    return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoriaId =
                   new SelectList(await _categoriaService.GetCategorias(), "Id", "Nome");
            ViewBag.Erro = "Erro ao criar Produto";


            return View(produtoVM);
        }

        [HttpGet]
        public async Task<IActionResult> DetalhesProduto(int id)
        {
            var produto = await _produtoService.GetProdutoPorId(id, ObterTokenJwt());

            if (produto is null)
                return View("Error");

            return View(produto);
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarProduto(int id)
        {
            var result = await _produtoService.GetProdutoPorId(id, ObterTokenJwt());

            if (result is null)
                return View("Error");

            ViewBag.CategoriaId =
                new SelectList(await _categoriaService.GetCategorias(), "Id", "Nome");

            return View(result);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoViewModel>> AtualizarProduto(int id, ProdutoViewModel produtoVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _produtoService.AtualizarProduto(id, produtoVM, ObterTokenJwt());

                if (result)
                    return RedirectToAction(nameof(Index));
            }

            return View(produtoVM);
        }

        [HttpGet]
        public async Task<IActionResult> DeletarProduto(int id)
        {
            var result = await _produtoService.GetProdutoPorId(id, ObterTokenJwt());

            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpPost(), ActionName("DeletarProduto")]
        public async Task<IActionResult> DeletarConfirmado(int id)
        {
            var result = await _produtoService.DeletarProduto(id, ObterTokenJwt());

            if (result)
                return RedirectToAction(nameof(Index));

            return View("Error");
        }

        private string ObterTokenJwt()
        {
            if (HttpContext.Request.Cookies.ContainsKey("X-Access-Token"))
            {
                _token = HttpContext.Request.Cookies["X-Access-Token"].ToString();
                return _token;
            }

            return _token;
        }
    }
}
