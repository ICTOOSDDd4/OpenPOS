using OpenPOS_Controllers;
using OpenPOS_Models;
using OpenPOS_Settings.Exceptions;

namespace OpenPOS_APP.Resources.Controls;

public partial class OrderView : ContentView
{
    public Order Order;
    public HorizontalStackLayout HorizontalLayout;
    public event EventHandler OrderDone;
    public event EventHandler OrderCanceled;

    private readonly TableController _tableController;
    private readonly OrderController _orderController;

    public OrderView()
    {
        InitializeComponent();
        _tableController = new TableController();
        _orderController = new OrderController();

        MainVerticalLayout.Shadow = new Shadow
        {
            Offset = new Point(5, 5),
            Brush = Brush.Black,
            Opacity = 0.12f,
        };
    }

   private void OnClickedDone(object sender, EventArgs e)
   {
      try
      {
         if (OrderDone != null)
         {
            OrderDone.Invoke(this, e);
         }
         else
         {
            throw new Exception("OrderDone event is not set");
         }
      }
      catch (Exception ex)
      {
         ExceptionHandler.HandleException(ex, null, true, false);
      }
      
   }

   private void OnClickedCancel(object sender, EventArgs e)
   {
      try
      {
         if (OrderCanceled != null)
         {
            OrderCanceled.Invoke(this, e);
         }
         else
         {
            throw new Exception("OrderCanceled event is not set");
         }
      }
      catch (Exception ex)
      {
         ExceptionHandler.HandleException(ex, null, true, false);
      }
   }

   public void AddBinds(Order o, HorizontalStackLayout h)
   {
      Order = o;
      HorizontalLayout = h;
      OrderNUmber.Text = $"Order: {Order.Id}";
      Table table = _tableController.GetByBillId(Order.Bill_id);
      TableNumber.Text = $"Table: {table.Table_number}";
      AddOrderLinesToLayout();
      
   }

    private void AddOrderLinesToLayout()
    {
        foreach (OrderLineProduct line in _orderController.GetOrderLines(Order.Id))
        {
            // Setting up layout
            HorizontalStackLayout layout = new()
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Spacing = 10,
                Margin = new Thickness(2)
            };

            // Adding product
            Label productLabel = new()
            {
                Text = line.Name
            };
            layout.Add(productLabel);

            // Adding amount
            Label amountLabel = new()
            {
                Text = $"{line.Amount}"
            };
            layout.Add(amountLabel);

            // Adding layout to main layout.
            OrderLinesLayout.Add(layout);
        }
    }
}