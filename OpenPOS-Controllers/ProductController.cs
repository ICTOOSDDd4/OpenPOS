using OpenPOS_Models;
using OpenPOS_Database.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPOS_Controllers
{
    public class ProductController
    {
        private ProductService _produstService = new();

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
            else
            {
                return _produstService.GetAllByFilter(searchString);
            }
        }
    }
}
