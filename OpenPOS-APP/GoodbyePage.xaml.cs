using OpenPOS_APP.Models;
using OpenPOS_APP.Settings;

namespace OpenPOS_APP;

public partial class GoodbyePage : ContentPage
{
	private System.Timers.Timer _timer = new System.Timers.Timer(1000);
	private int count = 10;
	public GoodbyePage()
	{
		InitializeComponent();
		ApplicationSettings.LoggedinUser = null;
      ApplicationSettings.CurrentBill.Paid = true;
		BillService.Update(ApplicationSettings.CurrentBill);
		ApplicationSettings.CurrentBill = null;
		ApplicationSettings.CheckoutList = new();
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
