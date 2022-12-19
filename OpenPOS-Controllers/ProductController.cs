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
        
        public List<Product> GetAllProducts()
        {
            return _produstService.GetAll();
        }

        public List<Product> GetProductsBySearch(string searchString) 
        {
            if (string.IsNullOrWhiteSpace(searchString) || string.IsNullOrEmpty(searchString))
            {
                return _produstService.GetAll();
            }
            return _produstService.GetAllByFilter(searchString);
        }
        
        public List<Product> GetByCategory(int categoryId)
        {
            return _produstService.GetAllByCategoryId(categoryId);
        }
        
        public Product GetProductById(int id)
        {
            return _produstService.FindByID(id);
        }
    }
}
