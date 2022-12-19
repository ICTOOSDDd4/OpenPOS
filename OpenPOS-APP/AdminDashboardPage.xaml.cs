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
		}
	}

	
}