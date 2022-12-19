using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.VisualElements;
using OpenPOS_Controllers;
using OpenPOS_Models;
using SkiaSharp;

namespace OpenPOS_APP.Resources.Controls;

public partial class OrderChart : ContentView
{
	private OrderController _orderController;
	public ISeries[] Series { get; set; }
	public string Title { get; set; } = "Orders";

	public OrderChart()
	{
		_orderController = new OrderController();
		InitializeComponent();
		CreateGraph();
	}

	private void CreateGraph()
	{
		var values = new List<int>();
		var labels = new List<string>();
		List<Order> lines = _orderController.GetAllOrders();
		var groupBy = lines.GroupBy(x => x.Created_At);
		foreach (var item in groupBy)
		{
			values.Add(item.Count());
			labels.Add(item.Key.ToString("MM-dd-yy"));
		}
		
		Series = new ISeries[]
		{ 
			new LineSeries<int>
			{
				Name = "Revenue",
				Values = values.ToArray(),
				Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 4 },
				Fill = null,
				GeometryFill = null,
				GeometryStroke = null
			}
		};


		List<Axis> XAxes = new List<Axis>
		{
			new Axis()
			{
				Labels = labels
			}
		};
		
		RChart.Series = Series;
		RChart.XAxes = XAxes;
	}
}