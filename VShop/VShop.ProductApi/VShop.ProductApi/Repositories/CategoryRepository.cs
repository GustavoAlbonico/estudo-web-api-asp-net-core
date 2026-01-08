using Mapster;
using Microsoft.EntityFrameworkCore;
using VShop.ProductApi.Context;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Models;
using VShop.ProductApi.Repositories.Interfaces;

namespace VShop.ProductApi.Repositories;

public class CategoryRepository(AppDbContext _context) : ICategoryRepository
{

    public async Task<IEnumerable<Category>> GetAll()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<IEnumerable<CategoryDTO>> GetCategoriesProducts()
    {
        return await _context.Categories.ProjectToType<CategoryDTO>().ToListAsync();
    }

    public async Task<Category> GetById(int id)
    {
        return await _context.Categories.Where(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Category> Create(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category> Update(Category category)
    {
        _context.Entry(category).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category> Delete(int id)
    {
        var product = await GetById(id);
        _context.Categories.Remove(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public Task<Category> GetCategoryById(int id)
    {
        throw new NotImplementedException();
    }
}
