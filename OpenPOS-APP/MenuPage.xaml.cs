using OpenPOS_APP.Models;
using OpenPOS_APP.Resources.Controls;
using OpenPOS_APP.Services.Models;
using System.Diagnostics;

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

	// something goes wrong with displaying the right products on search
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

	public virtual void OnSearch(object sender, EventArgs e) {
		MainVerticalLayout.Clear();
		Debug.WriteLine(((SearchBar)sender).Text);
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