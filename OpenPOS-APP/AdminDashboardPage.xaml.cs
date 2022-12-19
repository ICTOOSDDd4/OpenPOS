using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using OpenPOS_Controllers;
using SkiaSharp;

namespace OpenPOS_APP;

public partial class AdminDashboardPage : ContentPage
{
	private OrderController _orderController;
	public AdminDashboardPage()
	{
		_orderController = new OrderController();
		InitializeComponent();
		AdminHeader.CurrentPage = this;
	}

	
}