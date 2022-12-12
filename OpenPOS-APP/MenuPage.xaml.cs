using OpenPOS_APP.Resources.Controls;
using OpenPOS_APP.Resources.Controls.PopUps;
using OpenPOS_Controllers;
using OpenPOS_Models;
using OpenPOS_Settings.EventArgsClasses;
using CommunityToolkit.Maui.Views;

namespace OpenPOS_APP;

public partial class MenuPage : ContentPage
{
	  public List<Product> Products { get; set; }
    public Dictionary<int, int> SelectedProducts { get; set; }
	  public delegate void OnSearchEventHandler(object source, EventArgs args);

    private HorizontalStackLayout _horizontalLayout;
    private readonly ProductController _productController;
    private readonly CategoryController _categoryController;
    private readonly OrderController _orderController;
    private const int _productCardViewWidth = 300;
    private bool _isInitialized;
	  private double _width;

    public MenuPage()
    {
        _productController = new ProductController();
        _categoryController = new CategoryController();
        _orderController = new OrderController();

        SelectedProducts = new Dictionary<int, int>();
        Products = _productController.GetAllProducts();
        InitializeComponent();
        Header.Searched += OnSearch;
        Header.currentPage = this;
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        // Gets called by MAUI
        base.OnSizeAllocated(width, height);
        if (!_isInitialized)
        {
            _isInitialized = true;
            SetWindowScaling(width, height);
        }
    }

    private void SetWindowScaling(double width, double height)
	  {
        ScrView.HeightRequest = height - _productCardViewWidth;
        _width = width;
        AddAllCategories(_categoryController.GetAll());
        AddAllProducts();
    }

    public void AddAllProducts()
    {
        MainVerticalLayout.Clear();
        _horizontalLayout = null;
        foreach (var t in Products)
        {
            AddProductToLayout(t);
        }
    }

    public void AddProductToLayout(Product product)
    {
        int moduloNumber = ((int)_width / _productCardViewWidth);
        if (_horizontalLayout == null || _horizontalLayout.Children.Count % moduloNumber == 0)
        {
            AddHorizontalLayout();
        }

        ProductView productView = new();
        productView.SetProductValues(this, product);
        productView.ClickedMoreInfo += OnInfoButtonClicked;
        if (_horizontalLayout != null)
        {
            _horizontalLayout.Add(productView);
        }
        else
        {
            throw new Exception("Horizontal layout is null, Can't add product to layout");
        }
    }


    public void AddAllCategories(List<Category> categories)
    {
		//adds an "all" category
        CategoryView categoryView = new CategoryView();
        categoryView.SetCategoryValues(this, new Category() { Id = 0, Name = "All"});
        CategoryHorizontal.Add(categoryView);

        foreach (var t in categories)
        {
            AddCategoryToLayout(t);
        }
    }

    public void AddCategoryToLayout(Category category)
    {
        CategoryView categoryView = new CategoryView();
        categoryView.SetCategoryValues(this, category);
        CategoryHorizontal.Add(categoryView);
    }

    private void AddHorizontalLayout()
    {
        HorizontalStackLayout hLayout = new()
        {
            Spacing = 20,
            Margin = new Thickness(10)
        };
        MainVerticalLayout.Add(hLayout);
        _horizontalLayout = hLayout;
    }

    private async void OnInfoButtonClicked(object sender, InfoButtonEventArgs e)
    {
        ProductInfoPopUp infoPop = new();
        infoPop.SetProduct(e.product);
        this.ShowPopup(infoPop);
    }

    private async void OrderButton_OnClicked(object sender, EventArgs e)
    {
        if (SelectedProducts.Count == 0)
        {
            await DisplayAlert("No products selected", "You forgot to add products to your order!", "Back");

        }
        else
        {
            if (await DisplayAlert("Confirm order", "Are you sure you want to place your order?", "Yes", "No"))
            {
                _orderController.CreateOrder(SelectedProducts);
            }
            else
            {
                await DisplayAlert("Order Placed", "Your order was successfully sent to our staff!", "Thank you");
                await Shell.Current.GoToAsync(nameof(MenuPage));
            }
        }
    }

    public virtual void OnSearch(object sender, EventArgs e)
    {
        MainVerticalLayout.Clear();
        if (String.IsNullOrWhiteSpace(((SearchBar)sender).Text) || String.IsNullOrEmpty(((SearchBar)sender).Text))
        {
            Products = _productController.GetAllProducts();
        }
        else
        {
            Products = _productController.GetProductsBySearch(((SearchBar)sender).Text);
        }

        AddAllProducts();
    }
}
