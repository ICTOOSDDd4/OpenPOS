using OpenPOS_APP.Controllers;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Models;

namespace OpenPOS_APP;

public partial class OrderView : ContentView
{
   public Order order;
   public HorizontalStackLayout layout;
   public event EventHandler OrderDone;
   public event EventHandler OrderCanceled;
	public OrderView()
	{
		InitializeComponent();
      MainVerticalLayout.Shadow = new Shadow
      {
         Offset = new Point(5, 5),
         Brush = Brush.Black,
         Opacity = 0.12f,
      };
   }

   private void OnClickedDone(object sender, EventArgs e)
   {
      OrderDone.Invoke(this, e);
   }

   private void OnClickedCancel(object sender, EventArgs e)
   {
      OrderCanceled.Invoke(this, e);
   }

   public void AddBinds(Order o, HorizontalStackLayout h)
   {
      order = o;
      layout = h;
      OrderNUmber.Text = $"Order: {order.Id}";
      Table table = TableService.FindByBill(order.Bill_id);
      TableNumber.Text = $"Table: {table.Table_number}";
      AddOrderLinesToLayout();
   }

   private void AddOrderLinesToLayout()
   {
      foreach(OrderLine line in order.GetLines(order.Id)) 
      {
         // Setting up layout
         HorizontalStackLayout layout = new HorizontalStackLayout();
         layout.VerticalOptions = LayoutOptions.Center;
         layout.HorizontalOptions = LayoutOptions.Center;
         layout.Spacing = 10;
         layout.Margin = new Thickness(2);

         // Adding product
         Label productLabel = new Label();
         productLabel.Text = line.Name;
         layout.Add(productLabel);

         // Adding amount
         Label amountLabel = new Label();
         amountLabel.Text = $"{ line.Amount }";
         layout.Add(amountLabel);

         // Adding layout to main layout.
         OrderLinesLayout.Add(layout);
      }
   }

}