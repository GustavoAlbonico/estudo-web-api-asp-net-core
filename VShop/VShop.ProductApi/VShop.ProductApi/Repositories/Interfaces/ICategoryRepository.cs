using VShop.ProductApi.DTOs;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
        Task<IEnumerable<CategoryDTO>> GetCategoriesProducts();
        Task<Category> GetById(int id);
        Task<Category> Create(Category category);
        Task<Category> Update(Category category);
        Task<Category> Delete(int id);

    }
}
