using OpenPOS_Controllers;
using OpenPOS_Models;
using OpenPOS_Settings;

namespace OpenPOS_APP;

public partial class GoodbyePage : ContentPage
{
	private System.Timers.Timer _timer = new System.Timers.Timer(1000);
	private int count = 10;
	private BillController _billController = new BillController();
	private TableController _tableController = new TableController();
	public GoodbyePage()
	{
		InitializeComponent();
		
		// TODO: Need to become background tasks these can't stay in the constructor!
		ApplicationSettings.LoggedinUser = null;
		
		_billController.MarkAsPaid(ApplicationSettings.CurrentBill);
		ApplicationSettings.CurrentBill = null;
		
		ApplicationSettings.CheckoutList = new Dictionary<Product, int>();
		
		_tableController.RemoveBill(ApplicationSettings.TableNumber);
		ApplicationSettings.TableNumber = 0;
		
		_timer.Elapsed += Timer_Tick;
		_timer.Start();
	}
	
	private void Timer_Tick(object sender, object e)
	{
		Dispatcher.DispatchAsync(() =>
		{
         if (count == 0)
         {
            _timer.Stop();
            Device.BeginInvokeOnMainThread(RedirectToMainPage);
         }
         else
         {
            Countdown.Text = $"You will be signed out and redirected in {count} seconds";
            count--;
         }
      });	
	}
	
	

	private async void RedirectToMainPage()
	{
		await Shell.Current.GoToAsync(nameof(Onboarding));
	}

}
