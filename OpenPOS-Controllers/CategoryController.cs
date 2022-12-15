using OpenPOS_Models;
using OpenPOS_Database.ModelServices;

namespace OpenPOS_Controllers
{
    public  class CategoryController
    {
        private CategoryService _categoryService;

        public CategoryController()
        {
            _categoryService = new CategoryService();
        }
        // Create Method
        public Category CreateNew(string categoryName)
        {
            Category newCategory = new()
            {
                Name = categoryName
            };

            return _categoryService.Create(newCategory);
        }

        // Delete Method
        public bool Delete(Category category)
        {
            return _categoryService.Delete(category);
        }

        // Select Methods
        public List<Category> GetAll()
        {
            return _categoryService.GetAll();
        }
        
        public Category Get(int id)
        {
            return _categoryService.FindByID(id);
        }
        
        // Update Methods
        public bool UpdateAll(Category category)
        {
            return _categoryService.Update(category);
        }
        
        public bool UpdateName(int id, string name)
        {
            Category category = Get(id);
            category.Name = name;
            return _categoryService.Update(category);
        }
    }
}
