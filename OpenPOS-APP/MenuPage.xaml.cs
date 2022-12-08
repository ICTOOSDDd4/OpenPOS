using OpenPOS_APP.Models;
using OpenPOS_APP.Resources.Controls;
using OpenPOS_APP.Services.Models;
using OpenPOS_APP.Settings;
using OpenPOS_APP.EventArgsClasses;

namespace OpenPOS_APP;

public partial class MenuPage : ContentPage
{
	public List<Product> Products { get; set; }
	private HorizontalStackLayout _horizontalLayout;
	public Dictionary<int, int> SelectedProducts { get; set; }
	private HorizontalStackLayout HorizontalLayout;
	public delegate void OnSearchEventHandler(object source, EventArgs args);
  public static event OnSearchEventHandler Searched;
	private int _ProductCardViewWidth = 300;
	
	private bool _isInitialized;
	private double _width;
    public MenuPage()
	{
        SelectedProducts = new Dictionary<int, int>();
        Products = ProductService.GetAll();
		InitializeComponent();
		Header.Searched += OnSearch;
		Header.currentPage = this;
	}

	protected override void OnSizeAllocated(double width, double height)
	{ // Gets called by MAUI
		base.OnSizeAllocated(width, height);
		if (!_isInitialized)
		{
			_isInitialized = true;
			SetWindowScaling(width,height);
		}
        AddAllProducts();
    }

	private void SetWindowScaling(double width, double height)
	{
		ScrView.HeightRequest = height - _ProductCardViewWidth;
		_width = width;
		AddAllCategories(CategoryService.GetAll());
        AddAllProducts();
    }

	public void AddAllProducts()
	{
		MainVerticalLayout.Clear();
        _horizontalLayout = null;
		for (int i = 0; i < Products.Count; i++)
		{
            AddProductToLayout(Products[i]);
		}
	}

   public void AddProductToLayout(Product product)
   {
	   int moduloNumber = ((int)_width / _ProductCardViewWidth);
	   if (HorizontalLayout == null || HorizontalLayout.Children.Count % moduloNumber == 0) 
		{
			AddHorizontalLayout();
		}
        
		ProductView productView = new ProductView();
		productView.SetProductValues(this,product);
		productView.ClickedMoreInfo += OnInfoButtonClicked;
		HorizontalLayout.Add(productView);
    }

    public void AddAllCategories(List<Category> categories)
    {
		//adds an "all" category
        CategoryView categoryView = new CategoryView();
        categoryView.SetCategoryValues(this, new Category() { Id = 0, Name = "All"});
        CategoryHorizontal.Add(categoryView);

        for (int i = 0; i < categories.Count; i++)
        {
            AddCategoryToLayout(categories[i]);
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
      HorizontalStackLayout hLayout = new HorizontalStackLayout();
		hLayout.Spacing = 20;
		hLayout.Margin = new Thickness(10);
		MainVerticalLayout.Add(hLayout);
		_horizontalLayout = hLayout;
    }

	private async void OnInfoButtonClicked(object sender, EventArgs e)
	{
		await DisplayAlert("Work In Progress", "This will display more about the product and allergy information",
			"Understood");
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
				Order order = new Order(1, false, ApplicationSettings.LoggedinUser.Id, ApplicationSettings.CurrentBill.Id, DateTime.Now, DateTime.Now);
				order = OrderService.Create(order);

				// Get current product from selected products
				foreach (KeyValuePair<int, int> entry in SelectedProducts)
				{
					OrderLine line = new OrderLine(order.Id, entry.Key, entry.Value, "In Development");
					OrderLineService.Create(line);
				}
            if (order == null)
            {
               await DisplayAlert("Oops", "Something went wrong please try again.", "Alright");
            }
            else
            {
               await DisplayAlert("Order Placed", "Your order was successfully sent to our staff!", "Thank you");
               await Shell.Current.GoToAsync(nameof(MenuPage));
            }
         }
         else
         {
			 //EMPTY FOR NOW: DOING NOTHING
         }
        }		
	}

	public virtual void OnSearch(object sender, EventArgs e) {
		MainVerticalLayout.Clear();
		if (String.IsNullOrWhiteSpace(((SearchBar)sender).Text) || String.IsNullOrEmpty(((SearchBar)sender).Text))
		{
			Products = ProductService.GetAll();
		} else
		{
            Products = ProductService.GetAllByFilter(((SearchBar)sender).Text);
        }
		AddAllProducts();
	}
}
