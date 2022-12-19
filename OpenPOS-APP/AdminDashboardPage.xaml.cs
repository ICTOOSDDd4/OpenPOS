using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using OpenPOS_Controllers;
using SkiaSharp;

namespace OpenPOS_APP;

public partial class AdminDashboardPage : ContentPage
{
	public ISeries[] RevenueSeries { get; set; }
	public string RevenueTitle { get; set; }
	private OrderController _orderController;
	public AdminDashboardPage()
	{
		_orderController = new OrderController();
		RevenueTitle = "Revenue";
		InitializeComponent();
		AdminHeader.CurrentPage = this;
		CreateRevueneGraph();
	}

	private void CreateRevueneGraph()
	{
		// List<OrderLine> lines = _orderController.GetOrderLines();
		// foreach (OrderLine line in lines)
		// {
		// 	IN DEVELOPMENT
		// }
		RevenueSeries = new ISeries[]
		{ // Dev test data
			new LineSeries<double>
			{
				Values = new List<double> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
				Name = "Revenue",
				Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 4 },
				Fill = null,
				GeometryFill = null,
				GeometryStroke = null
			}
		};
		RevenueChart.Series = RevenueSeries;
	}
}