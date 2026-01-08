using MapsterMapper;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Models;
using VShop.ProductApi.Repositories.Interfaces;
using VShop.ProductApi.Services.Interfaces;

namespace VShop.ProductApi.Services;

public class ProductService(IMapper _mapper, IProductRepository _productRepository) :IProductService
{
    public async Task<IEnumerable<ProductDTO>> GetProducts()
    {
        return await _productRepository.GetAll();
    }

    public async Task<ProductDTO> GetProductById(int id)
    {
        return await _productRepository.GetById(id);
    }
    public async Task AddProduct(ProductDTO productDto)
    {
        var productEntity = _mapper.Map<Product>(productDto);
        await _productRepository.Create(productEntity);
        productDto.Id = productEntity.Id;
    }

    public async Task UpdateProduct(ProductDTO productDto)
    {
        var categoryEntity = _mapper.Map<Product>(productDto);
        await _productRepository.Update(categoryEntity);
    }

    public async Task RemoveProduct(int id)
    {
        var productEntity = await _productRepository.GetById(id);
        await _productRepository.Delete(productEntity.Id);
    }
}
