using System.Diagnostics;
using OpenPOS_Controllers;
using OpenPOS_Models;
using OpenPOS_Settings.EventArgsClasses;

namespace OpenPOS_APP;

public partial class OrderOverviewPage : ContentPage
{
    public List<Order> Orders { get; set; }
    private HorizontalStackLayout _horizontalLayout;
    private OpenPOSAPIController _openPOSAPIController;
   private OrderController _orderController;
    private bool _isInitialized;
    private double _width;

    public OrderOverviewPage()
    {
        _orderController = new OrderController();
        InitializeComponent();
        Orders = _orderController.GetOpenOrders();
        Initialize();
    }

    private async void Initialize()
    {
        await _openPOSAPIController.SubcribeToOrderNotification(newOrder);
    }

    private async void newOrder(object sender, OrderEventArgs orderEvent)
    {

       Debug.WriteLine("NewEvent");
       await Dispatcher.DispatchAsync(() =>
       { 
           Orders.Add(orderEvent.order); 
           AddOrderToLayout(orderEvent.order);
        });
    }

    protected override void OnSizeAllocated(double width, double height)
    {
      base.OnSizeAllocated(width, height);
      if (!_isInitialized)
      {
         _isInitialized = true;
         SetWindowScaling(width, height);
      }

    }

    private void SetWindowScaling(double width, double height)
    {
      ScrView.HeightRequest = height - 300;
      _width = width;
      AddAllOrders();

    }

    private void OrderCanceled(object sender, EventArgs e)
    {
	    OrderView view = (OrderView)sender;
	    Order order = view.order;
        order.Status = false;
        _orderController.UpdateOrder(order);
        DeleteView(view);
    }

    private void OrderDone(object sender, EventArgs e)
    {
      OrderView view = (OrderView)sender;
      Order order = view.order;
      order.Status = true;
      _orderController.UpdateOrder(order);
      DeleteView(view);
    }

    private void DeleteView(OrderView view)
    {
      view.layout.Children.Remove(view);
    }

    private async void ClickedRefresh(object sender, EventArgs e)
    {
      await Shell.Current.GoToAsync(nameof(OrderOverviewPage));
    }


    private void AddAllOrders()
    {
      for (int i = 0; i < Orders.Count; i++)
      {
         AddOrderToLayout(Orders[i]);
      }
    }

    public void AddOrderToLayout(Order order)
    {
      int moduloNumber = ((int)_width / 300);
      if (_horizontalLayout == null || _horizontalLayout.Children.Count % moduloNumber == 0)
      {
		    AddHorizontalLayout();
      }

      OrderView orderview = new OrderView(); 
      orderview.AddBinds(order, _horizontalLayout);
      orderview.OrderDone += OrderDone;
      orderview.OrderCanceled += OrderCanceled;
      _horizontalLayout.Add(orderview);      
    }

    private void AddHorizontalLayout()
    {
      HorizontalStackLayout hLayout = new HorizontalStackLayout();
      hLayout.Spacing = 20;
      hLayout.Margin = new Thickness(10);
      MainVerticalLayout.Add(hLayout);
      _horizontalLayout = hLayout;
    }
	
	
	
}