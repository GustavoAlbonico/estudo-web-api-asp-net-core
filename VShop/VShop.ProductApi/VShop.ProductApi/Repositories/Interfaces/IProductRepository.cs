using VShop.ProductApi.DTOs;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.Repositories.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<ProductDTO>> GetAll();
    Task<ProductDTO> GetById(int id);
    Task<Product> Create(Product product);
    Task<Product> Update(Product product);
    Task<Product> Delete(int id);
}
