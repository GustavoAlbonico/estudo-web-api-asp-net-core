using APICatalogo.DTOs;
using APICatalogo.Models;
using Mapster;

public class ProdutoDTOMappingConfig : IRegister
{

    //mapseter
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Produto, ProdutoDTO>().TwoWays();
        config.NewConfig<Categoria, CategoriaDTO>().TwoWays();
        config.NewConfig<Produto, ProdutoDTOUpdateRequest>().TwoWays();
        config.NewConfig<Produto, ProdutoDTOUpdateResponse>().TwoWays();
    }
}