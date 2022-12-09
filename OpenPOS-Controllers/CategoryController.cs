using OpenPOS_Models;
using OpenPOS_Database.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPOS_Controllers
{
    public static class CategoryController
    {
        private static CategoryService _categoryService = new();
        public static List<Category> GetAllCategories()
        {
            return _categoryService.GetAll();
        }
    }
}
