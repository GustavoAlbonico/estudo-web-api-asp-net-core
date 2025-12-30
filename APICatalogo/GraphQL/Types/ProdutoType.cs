using APICatalogo.Models;

namespace APICatalogo.GraphQL.Types;

public class ProdutoType : ObjectType<Produto>
{
    protected override void Configure(IObjectTypeDescriptor<Produto> descriptor)
    {
        descriptor.Ignore(p => p.CategoriaId);
    }
}
