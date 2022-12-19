using OpenPOS_Controllers;

namespace OpenPOS_APP;

public partial class AdminDashboardPage : ContentPage
{
	public string RevenueTitle { get; set; }
	private OrderController _orderController;
	public AdminDashboardPage()
	{
		_orderController = new OrderController();
		RevenueTitle = "Revenue";
		InitializeComponent();
		//AdminHeader.CurrentPage = this;
		CreateRevueneGraph();
	}

	private void CreateRevueneGraph()
	{

	}
}