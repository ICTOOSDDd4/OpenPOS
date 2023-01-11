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

        /// <summary>
        /// Creates a new Category
        /// </summary>
        /// <param name="categoryName">The CategoryName of the new Category</param>
        /// <returns>model with the newly created Category</returns>
        public Category CreateNew(string categoryName)
        {
            Category newCategory = new()
            {
                Name = categoryName
            };

            return _categoryService.Create(newCategory);
        }

        /// <summary>
        /// Deletes the Category by given CategoryId
        /// </summary>
        /// <param name="category">Category model</param>
        /// <returns>Bool if succeeded or not</returns>
        public bool Delete(Category category)
        {
            return _categoryService.Delete(category);
        }

        /// <summary>
        /// Gets all Categories
        /// </summary>
        /// <returns>List of all Categories</returns>
        public List<Category> GetAll()
        {
            return _categoryService.GetAll();
        }
        
        /// <summary>
        /// Gets a Category by Id
        /// </summary>
        /// <param name="id">CategoryId</param>
        /// <returns>Category model</returns>
        public Category Get(int id)
        {
            return _categoryService.FindByID(id);
        }
        
        /// <summary>
        /// Updates a Category to given Category model
        /// </summary>
        /// <param name="category">Category model</param>
        /// <returns>Bool if succeeded or not</returns>
        public bool UpdateAll(Category category)
        {
            return _categoryService.Update(category);
        }
        
        /// <summary>
        /// Updates the CategoryName of a Category by Id
        /// </summary>
        /// <param name="id">CategoryId</param>
        /// <param name="name">CategoryName</param>
        /// <returns>Bool if succeeded or not</returns>
        public bool UpdateName(int id, string name)
        {
            Category category = Get(id);
            category.Name = name;
            return _categoryService.Update(category);
        }
    }
}
