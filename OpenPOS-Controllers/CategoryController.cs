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

        // Create Method
        public static Category CreateNew(string CategoryName)
        {
            Category newCategory = new()
            {
                Name = CategoryName
            };

            return _categoryService.Create(newCategory);
        }

        // Delete Method
        public static bool Delete(Category category)
        {
            return _categoryService.Delete(category);
        }

        // Select Methods
        public static List<Category> GetAll()
        {
            return _categoryService.GetAll();
        }
        
        public static Category Get(int id)
        {
            return _categoryService.FindByID(id);
        }
        
        // Update Methods
        public static bool UpdateAll(Category category)
        {
            return _categoryService.Update(category);
        }
        
        public static bool UpdateName(int id, string name)
        {
            Category category = Get(id);
            category.Name = name;
            return _categoryService.Update(category);
        }
    }
}
