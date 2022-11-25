using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Models;

namespace OpenPOS_APP;

public partial class MenuPage : ContentPage
{
	public List<Product> Products { get; set; }
	public List<Product> SelectedProducts { get; set; }
	private HorizontalStackLayout HorizontalLayout;

	public MenuPage()
	{
      Products = ProductService.GetAll();
      InitializeComponent();
		SelectedProducts = new List<Product>();
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
		if (HorizontalLayout == null || HorizontalLayout.Children.Count % 8 == 0)
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