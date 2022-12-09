using OpenPOS_APP.Settings;

namespace OpenPOS_APP;

public partial class CategoryView : ContentView
{
    private MenuPage _menuPage;
    private Category _category;

    public CategoryView()
    {
        InitializeComponent();
    }

    public void SetCategoryValues(MenuPage page, Category category)
    {
        _menuPage = page;
        _category = category;

        CategoryButton.Text = category.Name;
    }

    private void OnClickedCategory(object sender, EventArgs e)
    {
        _menuPage.Products = ProductService.GetAllByCategoryId(_category.Id);
        _menuPage.AddAllProducts();
    }

}