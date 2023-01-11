using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using OpenPOS_Controllers;
using OpenPOS_Models;
using SkiaSharp;

namespace OpenPOS_APP.Resources.Controls;

public partial class RevenueChart : ContentView
{
   private OrderController _orderController;
   private ProductController _productController;
   private Dictionary<string, double> _revenueData;
   public double TotalPrice { get; set; }
   public ISeries[] Series { get; set; }
   public string Title { get; set; } = "Revenue";

   public RevenueChart()
	{
      _productController = new ProductController();
      _revenueData = new Dictionary<string, double>();
      _orderController = new OrderController();
      InitializeComponent();
      CreateGraph();
   }

   private void CreateGraph()
   { // TODO: Create a query for this
      List<OrderLine> lines = _orderController.GetOrderLines();
      foreach (OrderLine line in lines) // Processes all the data to be displayed in the graph element
      {
         double price = _productController.GetProductById(line.Product_id).Price * line.Amount;
         TotalPrice += price;
         DateTime created = _orderController.GetOrder(line.Order_id).Created_At;
         if (_revenueData.ContainsKey(created.Date.ToString("dd/MM/yyyy")))
         {
            _revenueData[created.Date.ToString("dd/MM/yyyy")] += price;
         }
         else 
         {
            _revenueData.Add(created.Date.ToString("dd/MM/yyyy"), price);
         }
         
      }
      
      Series = new ISeries[]
      { 
			new LineSeries<double> // Processes the data to be displayed as a line graph in the graph element
         {
            Name = "Revenue",
            Values = _revenueData.Values.ToArray(),
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
            Labels = _revenueData.Keys.ToArray() // Sets all the dates to the x-axis
         }
      };
      
      // Adds everything to the graph element
      RChart.Series = Series;
      RChart.XAxes = xAxes;
   }
}