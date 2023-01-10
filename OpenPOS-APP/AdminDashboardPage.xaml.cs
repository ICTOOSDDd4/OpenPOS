using OpenPOS_Controllers;
using OpenPOS_Database;
using OpenPOS_Models;

namespace OpenPOS_APP;

public partial class AdminDashboardPage : ContentPage
{
	private bool _isInitialized;
	public AdminDashboardPage()
	{
		InitializeComponent();
		AdminHeader.CurrentPage = this;
	}
	
	protected override void OnSizeAllocated(double width, double height)
	{
		double margin = VerticalStackLayout.Spacing * 3;
		// Gets called by MAUI
		base.OnSizeAllocated(width, height);
		if (!_isInitialized)
		{
			_isInitialized = true;
			double itemWidth = (width / 2) - margin;
			double itemHeight = (height / 2) - margin;
			SetGraphs(itemWidth, itemHeight);
			SetLists(itemWidth, itemHeight);
		}
	}
	
	private void SetGraphs(double itemWidth, double itemHeight)
	{
			
		RChart.WidthRequest = itemWidth;
		OChart.WidthRequest = itemWidth;

		RChart.HeightRequest = itemHeight;
		OChart.HeightRequest = itemHeight;
			
		string revenueValue = String.Format(((Math.Round(RChart.TotalPrice) == RChart.TotalPrice) ? "{0:0}" : "{0:0.00}"), RChart.TotalPrice);
		TotalRevenueLabel.Text = $"Total Revenue: € {revenueValue}";
		TotalOrderLabel.Text = "Total Amount of Orders: " + OChart.TotalAmount;
	}

	private void SetLists(double itemWidth, double itemHeight)
	{
		double width = itemWidth / 2;
		TopCategories.WidthRequest = width;
		TopProducts.WidthRequest = width;
		BottomCategories.WidthRequest = width;
		BottomProducts.WidthRequest = width;
		
		TopCategories.HeightRequest = itemHeight;
		TopProducts.HeightRequest = itemHeight;
		BottomCategories.HeightRequest = itemHeight;
		BottomProducts.HeightRequest = itemHeight;
		
	}

	
}