using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Models;
using OpenPOS_APP.Settings;
using System.Diagnostics;
using Microsoft.AspNetCore.SignalR.Client;
using OpenPOS_APP.NewFolder;
using OpenPOS_APP.Services;

namespace OpenPOS_APP;

public partial class MenuPage : ContentPage
{
	public List<Product> Products { get; set; }
	public Dictionary<Product, int> SelectedProducts { get; set; }
	private HorizontalStackLayout HorizontalLayout;
	
	private bool _isInitialized;
	private double _width;
    private EventHubService _eventHubService;
    public MenuPage()
	{
      Products = ProductService.GetAll();
      InitializeComponent();
      SelectedProducts = new Dictionary<Product, int>();
	}

	protected override void OnSizeAllocated(double width, double height)
	{
		base.OnSizeAllocated(width, height);
		if (!_isInitialized)
		{
			_isInitialized = true;
			SetWindowScaling(width,height);
		}
		
	}

	private void SetWindowScaling(double width, double height)
	{
		ScrView.HeightRequest = height - 300;
		_width = width;
		AddAllCategories(CategoryService.GetAll());
        AddAllProducts();
    }

	public void AddAllProducts()
	{
		MainVerticalLayout.Clear();
        HorizontalLayout = null;
		for (int i = 0; i < Products.Count; i++)
		{
            Debug.WriteLine(i);
            AddProductToLayout(Products[i]);
		}
	}

    public void AddProductToLayout(Product product)
    {
	   int moduloNumber = ((int)_width / 300);
	   if (HorizontalLayout == null || HorizontalLayout.Children.Count % moduloNumber == 0) 
       {
			AddHorizontalLayout();
        
		ProductView productView = new ProductView();
		productView.SetProductValues(this,product);
		productView.ClickedMoreInfo += OnInfoButtonClicked;
		HorizontalLayout.Add(productView);
        Debug.WriteLine(HorizontalLayout.Children.Count);
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
		HorizontalLayout = hLayout;
    }

	private async void OnInfoButtonClicked(object sender, EventArgs e)
	{
		await DisplayAlert("Work In Progress", "This will display more about the product and allergy information",
			"Understood");
	}

	// Temporary function to test Eventlisteners
    private async void ConnectButton_OnClicked(object sender, EventArgs e)
    {
        if (_eventHubService == null)
        {
			_eventHubService = new EventHubService();
        }

        if (!_eventHubService._isConnected)
        {
            _eventHubService.newOrder += newOrder;
            _eventHubService.ConnectToServer();
        }
    }

    // Temporary function to test Eventlisteners
    private void newOrder(object sender, OrderEventArgs orderEvent)
    {
		System.Diagnostics.Debug.WriteLine("NewEvent");
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
				foreach (KeyValuePair<Product, int> entry in SelectedProducts)
            {
					OrderLine line = new OrderLine(order.Id, entry.Key.Id, entry.Value, "In Development");
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
				// Empty for now, DOING NOTHING!
         }
      }		
	}
}