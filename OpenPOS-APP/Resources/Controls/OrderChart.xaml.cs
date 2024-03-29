using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using OpenPOS_Controllers;
using OpenPOS_Models;
using SkiaSharp;

namespace OpenPOS_APP.Resources.Controls;

public partial class OrderChart : ContentView
{
	private OrderController _orderController;
	private Dictionary<string, int> _orderData;
	public ISeries[] Series { get; set; }
	public int TotalAmount { get; set; }

	public OrderChart()
	{
		_orderData = new Dictionary<string, int>();
		_orderController = new OrderController();
		InitializeComponent();
		CreateGraph();
	}

	private void CreateGraph() // Processes all the necessary data and puts it into the graph element
	{
		List<Order> orders = _orderController.GetAllOrders();
		TotalAmount = orders.Count;
		foreach (var order in orders)
		{ // TODO: Create a query for this
			DateTime created = order.Created_At;
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
			new LineSeries<int> // Processes the created dataset to be display in the graph element
			{
				Name = "Orders",
				Values = _orderData.Values.ToArray(),
				Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 4 },
				Fill = null,
				GeometryFill = null,
				GeometryStroke = null
			}
		};


		List<Axis> xAxes = new List<Axis>
		{
			new()
			{
				Labels = _orderData.Keys.ToArray() // Adds the dates to the x-axis
			}
		};
		
		// Adds the data to the graph element
		OChart.Series = Series;
		OChart.XAxes = xAxes;
	}
}