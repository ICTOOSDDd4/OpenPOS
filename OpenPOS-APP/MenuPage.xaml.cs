using OpenPOS_APP.Models;
using OpenPOS_APP.Resources.Controls;
using OpenPOS_APP.Services.Models;

namespace OpenPOS_APP;

public partial class MenuPage : ContentPage
{
	public List<Product> Products { get; set; }
	public List<Product> SelectedProducts { get; set; }
	private HorizontalStackLayout HorizontalLayout;
    
	public delegate void OnSearchEventHandler(object source, EventArgs args);
    public static event OnSearchEventHandler Searched;
    public MenuPage()
	{
      Products = ProductService.GetAll();
      InitializeComponent();
		SelectedProducts = new List<Product>();
      AddAllProducts();
		Header.Searched += OnSearch;
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


	private void OrderButton_OnClicked(object sender, EventArgs e)
	{
		foreach (var VARIABLE in MainVerticalLayout.ToList())
		{
			//remove all
			MainVerticalLayout.Remove(VARIABLE);
		}
	}

	public virtual void OnSearch(object sender, EventArgs e, List<Product> products) {
		SelectedProducts.Clear();
		SelectedProducts = products;
		
		AddAllProducts();
	}
}