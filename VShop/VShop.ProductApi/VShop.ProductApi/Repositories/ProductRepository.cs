using Mapster;
using Microsoft.EntityFrameworkCore;
using VShop.ProductApi.Context;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Models;
using VShop.ProductApi.Repositories.Interfaces;

namespace VShop.ProductApi.Repositories
{
    public class ProductRepository(AppDbContext _context) : IProductRepository
    {
        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            var products = await _context.Products.ProjectToType<ProductDTO>().ToListAsync();
            return products;
        }

        public async Task<ProductDTO> GetById(int id)
        {
            return await _context.Products.ProjectToType<ProductDTO>()
                                          .Where(p => p.Id == id)
                                          .FirstOrDefaultAsync();
        }

        public async Task<Product> Create(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Delete(int id)
        {
            var product = await _context.Products
                                        .Where(p => p.Id == id)
                                        .FirstOrDefaultAsync();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
