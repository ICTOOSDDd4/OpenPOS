using OpenPOS_Controllers;
using OpenPOS_Models;
using OpenPOS_Settings;

namespace OpenPOS_APP;

public partial class GoodbyePage : ContentPage
{
	private readonly System.Timers.Timer _timer = new(1000);
	private int _count = 10;
	private readonly BillController _billController = new();
	private readonly TableController _tableController = new();

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
            if (_count == 0)
            {
                _timer.Stop();
                Dispatcher.Dispatch(RedirectToMainPage); //Possible fix
                // Device.BeginInvokeOnMainThread(RedirectToMainPage); //TODO: Fix this warning
            }
            else
            {
                Countdown.Text = $"You will be signed out and redirected in {_count} seconds";
                _count--;
            }
        });
    }

    private async void RedirectToMainPage()
	{
		await Shell.Current.GoToAsync(nameof(Onboarding));
	}
}
