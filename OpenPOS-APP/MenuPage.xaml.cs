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
	private HorizontalStackLayout _horizontalLayout;
	
	private bool _isInitialized;
	private double _width;
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
        _horizontalLayout = null;
		for (int i = 0; i < Products.Count; i++)
		{
            Debug.WriteLine(i);
            AddProductToLayout(Products[i]);
		}
	}

    public void AddProductToLayout(Product product)
    {
	   int moduloNumber = ((int)_width / 300);
       if (_horizontalLayout == null || _horizontalLayout.Children.Count % moduloNumber == 0)
       {
           AddHorizontalLayout();

           ProductView productView = new ProductView();
           productView.SetProductValues(this, product);
           productView.ClickedMoreInfo += OnInfoButtonClicked;
           _horizontalLayout.Add(productView);
           Debug.WriteLine(_horizontalLayout.Children.Count);
       }
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

}