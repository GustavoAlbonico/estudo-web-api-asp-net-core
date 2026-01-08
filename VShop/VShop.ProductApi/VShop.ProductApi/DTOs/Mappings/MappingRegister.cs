using Mapster;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.DTOs.Mappings
{
    public class MappingRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Category, CategoryDTO>().TwoWays();
            config.NewConfig<Product, ProductDTO>().TwoWays();

            
        }
    }
}
