using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using OpenPOS_Controllers;
using SkiaSharp;

namespace OpenPOS_APP.Resources.Controls;

public partial class RevenueChart : ContentView
{
   private OrderController _orderController;
   public ISeries[] Series { get; set; }
   public string Title { get; set; } = "Revenue";

   public RevenueChart()
	{
      _orderController = new OrderController();
      InitializeComponent();
      CreateGraph();
   }

   private void CreateGraph()
   {
      // List<OrderLine> lines = _orderController.GetOrderLines();
      // foreach (OrderLine line in lines)
      // {
      // 	IN DEVELOPMENT
      // }
      
      // Dev test data
      var values = new int[100];
      var r = new Random();
      var t = 0;

      for (var i = 0; i < 100; i++)
      {
         t += r.Next(-90, 100);
         values[i] = t;
      }
      Series = new ISeries[]
      { 
			new LineSeries<int>
         {
            Name = "Revenue",
            Values = values,
            Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 4 },
            Fill = null,
            GeometryFill = null,
            GeometryStroke = null
         }
      };
      RChart.Series = Series;
   }
}