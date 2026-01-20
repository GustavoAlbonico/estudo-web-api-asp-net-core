using Mapster;
using VShop.CartApi.Models;

namespace VShop.CartApi.DTOs.Mappings;

public class MappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Cart, CartDTO>().TwoWays();
        config.NewConfig<CartHeader, CartHeaderDTO>().TwoWays();
        config.NewConfig<CartItemDTO, CartItem>().TwoWays();
        config.NewConfig<Product, ProductDTO>().TwoWays();
    }
}
