using OpenPOS_Controllers;
using OpenPOS_Models;
using OpenPOS_Settings.EventArgsClasses;
using OpenPOS_Settings.Exceptions;
using Plugin.Maui.Audio;

namespace OpenPOS_APP;

public partial class OrderOverviewPage : ContentPage
{
    public List<Order> Orders { get; set; }
    public Dictionary<Order, List<OrderLineProduct>> OrderLines { get; set; }
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
        OrderLines = new Dictionary<Order, List<OrderLineProduct>>();
        Initialize();
    }

    private async void Initialize()
    {
        try
        {
            await _openPosApiController.SubscribeToOrderNotification(NewOrder);
        } catch (Exception e)
        {
            ExceptionHandler.HandleException(e, this, true, true);
        }
    }

    private async void NewOrder(object sender, OrderEventArgs orderEvent)
    {
        await Dispatcher.DispatchAsync(() =>
        { 
            System.Diagnostics.Debug.WriteLine("Received");
            Orders.Add(orderEvent.order); 
            AddOrderToLayout(orderEvent.order);
        });
        var audioPlayer = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("service-bell-ring.mp3"));

        audioPlayer.Play();
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

        _orderController.OrderLinesToDone(OrderLines[order], order);

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
            try
            {
                AddOrderToLayout(t);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, this, true, true);
            }
        }
    }

    public void AddOrderToLayout(Order order)
    {
        OrderLines.Add(order, _orderController.GetOrderLines(order.Id));
        
        int moduloNumber = ((int)_width / 300);
        if (_horizontalLayout == null || _horizontalLayout.Children.Count % moduloNumber == 0)
        {
            AddHorizontalLayout();
        }

        OrderView orderView = new OrderView();
        orderView.AddBinds(order, _horizontalLayout, OrderLines[order]);
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