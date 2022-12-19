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
   private double _totalPrice;
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
   {
      List<OrderLine> lines = _orderController.GetOrderLines();
      foreach (OrderLine line in lines)
      {
         double price = _productController.GetProductById(line.Product_id).Price * line.Amount;
         _totalPrice += price;
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
			new LineSeries<double>
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
         new Axis()
         {
            Labels = _revenueData.Keys.ToArray()
         }
      };
      
      
      
      RChart.Series = Series;
      RChart.XAxes = xAxes;
   }
}