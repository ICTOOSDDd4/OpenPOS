using System.Diagnostics;
using OpenPOS_Controllers;
using OpenPOS_Models;
using OpenPOS_Settings.EventArgsClasses;

namespace OpenPOS_APP;

public partial class OrderOverviewPage : ContentPage
{
    public List<Order> Orders { get; set; }
    private HorizontalStackLayout _horizontalLayout;
    private readonly OpenPosApiController _openPosApiController;
    private readonly OrderController _orderController;
    private bool _isInitialized;
    private double _width;

    public OrderOverviewPage()
    {
        _openPosApiController = new OpenPosApiController();
        _orderController = new OrderController();
        InitializeComponent();
        Orders = _orderController.GetOpenOrders();
        Initialize();
    }

    private async void Initialize()
    {
        await _openPosApiController.SubscribeToOrderNotification(NewOrder);
    }

    private async void NewOrder(object sender, OrderEventArgs orderEvent)
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
	    Order order = view.Order;
        order.Status = false;
        _orderController.UpdateOrder(order);
        DeleteView(view);
    }

    private void OrderDone(object sender, EventArgs e)
    {
        OrderView view = (OrderView)sender;
        Order order = view.Order;
        order.Status = true;
        _orderController.UpdateOrder(order);
        DeleteView(view);
    }

    private void DeleteView(OrderView view)
    {
        view.HorizontalLayout.Children.Remove(view);
    }

    private void AddAllOrders()
    {
        foreach (var t in Orders)
        {
            AddOrderToLayout(t);
        }
    }

    public void AddOrderToLayout(Order order)
    {
        int moduloNumber = ((int)_width / 300);
        if (_horizontalLayout == null || _horizontalLayout.Children.Count % moduloNumber == 0)
        {
            AddHorizontalLayout();
        }

        OrderView orderView = new OrderView();
        orderView.AddBinds(order, _horizontalLayout);
        orderView.OrderDone += OrderDone;
        orderView.OrderCanceled += OrderCanceled;

        if (_horizontalLayout != null)
        {
            _horizontalLayout.Add(orderView);
        }
        else
        {
            throw new Exception("HorizontalLayout is null, Can't add OrderView");
        }
    }

    private void AddHorizontalLayout()
    {
        HorizontalStackLayout hLayout = new HorizontalStackLayout
        {
            Spacing = 20,
            Margin = new Thickness(10)
        };
        MainVerticalLayout.Add(hLayout);
        _horizontalLayout = hLayout;
    }
}