using APICatalogo.Models;
using AutoMapper;

namespace APICatalogo.DTOs.Mappings;

public class ProdutoDTOMappingProfile : Profile
{
    //auto mapper
    public ProdutoDTOMappingProfile()
    {
        CreateMap<Produto, ProdutoDTO>().ReverseMap();
        CreateMap<Categoria, CategoriaDTO>().ReverseMap();
        CreateMap<Produto, ProdutoDTOUpdateRequest>().ReverseMap();
        CreateMap<Produto, ProdutoDTOUpdateResponse>().ReverseMap();
    }
}
