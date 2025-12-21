
using CategoriasMvc.Models;

namespace CategoriasMvc.Services.Interfaces;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoViewModel>> GetProdutos(string token);
    Task<ProdutoViewModel> GetProdutoPorId(int id,string token);
    Task<ProdutoViewModel> CriarProduto(ProdutoViewModel produtoVM, string token);
    Task<bool> AtualizarProduto(int id, ProdutoViewModel produtoVM, string token);
    Task<bool> DeletarProduto(int id, string token);
}
