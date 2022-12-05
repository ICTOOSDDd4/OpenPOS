using OpenPOS_APP.Settings;

namespace OpenPOS_APP;

public partial class GoodbyePage : ContentPage
{
	private System.Timers.Timer _timer = new System.Timers.Timer(2000);
	public GoodbyePage()
	{
		InitializeComponent();
		ApplicationSettings.LoggedinUser = null;
		_timer.Elapsed += Timer_Tick;
		_timer.Start();
	}
	
	private void Timer_Tick(object sender, object e)
	{
		_timer.Stop();
		Device.BeginInvokeOnMainThread(RedirectToMainPage);
	}
	
	

	private async void RedirectToMainPage()
	{
		await Shell.Current.GoToAsync(nameof(Onboarding));
	}

}
