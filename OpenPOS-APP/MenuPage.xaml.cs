using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Models;

namespace OpenPOS_APP;

public partial class MenuPage : ContentPage
{
	public List<Product> Products { get; set; }
	public List<Product> SelectedProducts { get; set; }
	private HorizontalStackLayout HorizontalLayout;
	
	private bool _isInitialized;
	private double _width;

	public MenuPage()
	{
      Products = ProductService.GetAll();
      InitializeComponent();
      SelectedProducts = new List<Product>();
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
		AddAllProducts();

	}

	void AddAllProducts()
	{
		for (int i = 0; i < Products.Count; i++)
		{
			AddProductToLayout(Products[i]);
		}
	}

   public void AddProductToLayout(Product product)
   {
	   int moduloNumber = ((int)_width / 300);
	   Console.WriteLine(moduloNumber + " " + _width);
		if (HorizontalLayout == null || HorizontalLayout.Children.Count % moduloNumber == 0) 
		{
			AddHorizontalLayout();
		}
		ProductView productView = new ProductView();
		productView.SetProductValues(this,product);
		HorizontalLayout.Add(productView);
	}

	private void AddHorizontalLayout()
	{
      HorizontalStackLayout hLayout = new HorizontalStackLayout();
		hLayout.Spacing = 20;
		hLayout.Margin = new Thickness(10);
		MainVerticalLayout.Add(hLayout);
		HorizontalLayout = hLayout;
   }


	private async void OrderButton_OnClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(CheckoutOverview));
	}
}