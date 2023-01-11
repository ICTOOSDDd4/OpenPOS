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
    public class ProductController
    {
        private ProductService _produstService;
        
        public ProductController()
        {
            _produstService = new ProductService();
        }
        
        /// <summary>
        /// Gets all Products
        /// </summary>
        /// <returns>List of all Products</returns>
        public List<Product> GetAllProducts()
        {
            return _produstService.GetAll();
        }

        /// <summary>
        /// Gets all Products that fit the searchString
        /// </summary>
        /// <param name="searchString">The filter searched for</param>
        /// <returns>List of all Products fitting to the searchString</returns>
        public List<Product> GetProductsBySearch(string searchString) 
        {
            if (string.IsNullOrWhiteSpace(searchString) || string.IsNullOrEmpty(searchString))
            {
                return _produstService.GetAll();
            }
            return _produstService.GetAllByFilter(searchString);
        }
        
        /// <summary>
        /// Gets all Products that are bound to the Category
        /// </summary>
        /// <param name="categoryId">The CategroyId searched</param>
        /// <returns>List of Products bound to the searched Category</returns>
        public List<Product> GetByCategory(int categoryId)
        {
            return _produstService.GetAllByCategoryId(categoryId);
        }
        
        /// <summary>
        /// Gets a Product by given Id
        /// </summary>
        /// <param name="id">The ProductId searched for</param>
        /// <returns>Product model</returns>
        public Product GetProductById(int id)
        {
            return _produstService.FindByID(id);
        }
        
        
    }
}
