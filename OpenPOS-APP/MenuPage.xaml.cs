using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Models;

namespace OpenPOS_APP;

public partial class MenuPage : ContentPage
{
	public List<Product> Products { get; set; }
	public List<Product> SelectedProducts { get; set; }
	public MenuPage()
	{
		Products = ProductService.GetAll();
		InitializeComponent();

		for (int i = 0; i < Products.Count; i++)
		{
			AddProductToLayout(Products[i]);
		}
		
	}

	public void AddProductToLayout(Product product)
	{
		ProductView productView = new ProductView();
		productView.SetProductValues(this,product);
		hLayout.Add(productView);
	}
	
	
	
}