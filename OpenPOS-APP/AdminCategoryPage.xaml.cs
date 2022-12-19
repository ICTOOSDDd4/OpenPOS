using OpenPOS_Controllers;
using OpenPOS_Models;

namespace OpenPOS_APP;

public partial class AdminCategoryPage : ContentPage
{
	public List<Category> Categories { get; set; }

	
	private readonly CategoryController _categoryController;
	private HorizontalStackLayout _horizontalLayout;
	
	private const int CategoryItemViewWidth = 500;
	private bool _isInitialized;
	private double _width;

	private int CategoryCounter;
	public AdminCategoryPage()
	{
		_categoryController = new CategoryController();
		Categories = _categoryController.GetAll();
		InitializeComponent();
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
		ScrView.HeightRequest = height - CategoryItemViewWidth;
		_width = width;
		AddAllCategoryItems(_categoryController.GetAll());
	}
	
	private void AddAllCategoryItems(List<Category> categories)
	{
		MainVerticalLayout.Clear();
		_horizontalLayout = null;
		foreach (var category in categories)
		{
			AddCategoryItemToLayout(category);
		}
	}

	private void AddCategoryItemToLayout(Category category)
	{
		if (_horizontalLayout == null || CategoryCounter == 2)
		{
			AddHorizontalLayout();
		}

		AdminCategoryView categoryView = new();
		categoryView.SetCategoryValue(this, category);

		if (_horizontalLayout != null) _horizontalLayout.Add(categoryView);
		CategoryCounter++;
	}
	
	private void AddHorizontalLayout()
	{
		CategoryCounter = 0;
		HorizontalStackLayout hLayout = new()
		{
			Spacing = 20,
			Margin = new Thickness(10)
		};
		MainVerticalLayout.Add(hLayout);
		_horizontalLayout = hLayout;
	}
}
