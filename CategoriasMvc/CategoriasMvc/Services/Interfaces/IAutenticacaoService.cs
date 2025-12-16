using CategoriasMvc.Models;

namespace CategoriasMvc.Services.Interfaces
{
    public interface IAutenticacaoService
    {
        Task<TokenViewModel> AutenticarUsuario(UsuarioViewModel usuarioVM);
    }
}
