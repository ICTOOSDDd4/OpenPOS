using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace OpenPOS_APP;

public partial class AdminDashboardPage : ContentPage
{
	public AdminDashboardPage()
	{
		InitializeComponent();
		AdminHeader.CurrentPage = this;
		SettingChartData();
	}

	private void SettingChartData()
	{
		RevenueChart.Series = new ISeries[]
		{
			new LineSeries<double>
			{
				Values = new double[] { 2, 1, 3, 5, 3, 4, 6 },
				Fill = null
			}
		};
	}
}