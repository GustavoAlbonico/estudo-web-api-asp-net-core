using CategoriasMvc.Models;
using CategoriasMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CategoriasMvc.Controllers
{
    public class AccountController : Controller
    {

        private readonly IAutenticacaoService _autenticacaoService;

        public AccountController(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UsuarioViewModel usuarioVM)
        {
            //verifica se o modelo é válido
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Login inválido....");
                return View(usuarioVM);
            }

            //verifica as credenciais do usuário e retorna um valor
            var result = await _autenticacaoService.AutenticarUsuario(usuarioVM);

            if(result is null)
            {
                ModelState.AddModelError(string.Empty, "Login inválido....");
                return View(usuarioVM);
            }

            Response.Cookies.Append("X-Access-Token", result.Token, new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            });

            return Redirect("/");
        }
    }
}
