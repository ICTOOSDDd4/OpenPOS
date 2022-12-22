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
	private ProductController _productController;
	private Dictionary<string, int> _orderData;
	public ISeries[] Series { get; set; }
	public string Title { get; set; } = "Orders";

	public OrderChart()
	{
		_orderData = new Dictionary<string, int>();
		_productController = new ProductController();
		_orderController = new OrderController();
		InitializeComponent();
		CreateGraph();
	}

	private void CreateGraph()
	{
		List<OrderLine> lines = _orderController.GetOrderLines();
		foreach (var item in lines)
		{ // TODO: Create a query for this
			DateTime created = _orderController.GetOrder(item.Order_id).Created_At;
			if (_orderData.ContainsKey(created.Date.ToString("dd/MM/yyyy")))
			{
				_orderData[created.Date.ToString("dd/MM/yyyy")]++;
			}
			else 
			{
				_orderData.Add(created.Date.ToString("dd/MM/yyyy"), 1);
			}
		}
		
		Series = new ISeries[]
		{ 
			new LineSeries<int>
			{
				Name = "Revenue",
				Values = _orderData.Values.ToArray(),
				Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 4 },
				Fill = null,
				GeometryFill = null,
				GeometryStroke = null
			}
		};


		List<Axis> xAxes = new List<Axis>
		{
			new Axis()
			{
				Labels = _orderData.Keys.ToArray()
			}
		};
		
		OChart.Series = Series;
		OChart.XAxes = xAxes;
	}
}