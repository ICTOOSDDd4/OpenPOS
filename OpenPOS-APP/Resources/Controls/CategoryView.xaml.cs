using OpenPOS_Controllers;
using OpenPOS_Models;

namespace OpenPOS_APP.Resources.Controls;

public partial class CategoryView : ContentView
{
    private MenuPage _menuPage;
    private Category _category;
    private ProductController _productController;

    public CategoryView()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Sets all values for the Category View, this method is to be called before adding the view to a layout!
    /// </summary>
    /// <param name="page">Current menu page</param>
    /// <param name="category">Selected category</param>
    public void SetCategoryValues(MenuPage page, Category category)
    {
        _productController = new ProductController();
        
        _menuPage = page;
        _category = category;

        CategoryButton.Text = category.Name;
    }

    private void OnClickedCategory(object sender, EventArgs e)
    {
        _menuPage.Products = _productController.GetByCategory(_category.Id);
        _menuPage.AddAllProducts();
    }

}