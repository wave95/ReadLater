using Models;
using System.Collections.Generic;

namespace Services
{
    public interface ICategoryService
    {
        CategoryModel CreateCategory(CategoryModel category);
        List<CategoryModel> GetCategories();
        CategoryModel GetCategory(int Id);
        CategoryModel GetCategory(string Name);
        void UpdateCategory(CategoryModel category);
        void DeleteCategory(int Id);
    }
}
