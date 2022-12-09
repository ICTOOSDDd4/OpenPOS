using OpenPOS_Models;
using OpenPOS_Database.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenPOS_Database.ModelServices;

namespace OpenPOS_Controllers
{
    public static class CategoryController
    {
        private static CategoryService _categoryService = new();

        public static Category CreateNewCategory(string CategoryName)
        {
            Category newCategory = new()
            {
                Name = CategoryName
            };

            return _categoryService.Create(newCategory);
        }
        
        public static bool CreateCategory(Category category)
        {
            return _categoryService.Update(category);
        }
        
        public static bool DeleteCategory(Category category)
        {
            return _categoryService.Delete(category);
        }

        public static List<Category> GetAllCategories()
        {
            return _categoryService.GetAll();
        }
        
        public static Category GetCategory(int id)
        {
            return _categoryService.FindByID(id);
        }
    }
}
