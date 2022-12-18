using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace OpenPOS_APP;

public partial class AdminDashboardPage : ContentPage
{
	public ISeries[] RevenueSeries { get; set; } 
		= new ISeries[]
	{
		new LineSeries<int>
		{
			Values = new int[] { 4, 6, 5, 3, -3, -1, 2 }
		},
		new ColumnSeries<double>
		{
			Values = new double[] { 2, 5, 4, -2, 4, -3, 5 }
		}
	};
	public AdminDashboardPage()
	{
		InitializeComponent();
		AdminHeader.CurrentPage = this;
	}
}