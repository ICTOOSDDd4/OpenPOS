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
		// Gets called by MAUI
		base.OnSizeAllocated(width, height);
		if (!_isInitialized)
		{
			_isInitialized = true;
			double margin = VerticalStackLayout.Spacing * 3;
			
			double itemWidth = (width / 2) - margin;
			RChart.WidthRequest = itemWidth;
			OChart.WidthRequest = itemWidth;
			

			double itemHeight = (height / 2) - margin;
			RChart.HeightRequest = itemHeight;
			OChart.HeightRequest = itemHeight;
			
			string revenueValue = String.Format(((Math.Round(RChart.TotalPrice) ==RChart.TotalPrice) ? "{0:0}" : "{0:0.00}"), RChart.TotalPrice);
			TotalRevenueLabel.Text = $"Total Revenue: € {revenueValue}";
			TotalOrderLabel.Text = "Total Amount of Orders: " + OChart.TotalAmount;
		}
	}

	
}