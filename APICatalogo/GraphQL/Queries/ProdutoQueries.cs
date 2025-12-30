using APICatalogo.Context;
using APICatalogo.Models;

namespace APICatalogo.GraphQL.Queries;

[ExtendObjectType("Query")]
public class ProdutoQueries
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Produto> GetProdutos([Service] AppDbContext context) => context.Produtos;
}