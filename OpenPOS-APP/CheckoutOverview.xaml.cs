using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Models;
using OpenPOS_APP.Settings;

namespace OpenPOS_APP;

public partial class CheckoutOverview : ContentPage
{
    private List<Product> CheckoutItems;
    public CheckoutOverview()
	{
		InitializeComponent();

		AddToCheckOut(ApplicationSettings.CheckoutList);

	}

    public void AddToCheckOut(List<Product> products)
    {
        CheckoutItems = products;
		CheckoutListView.ItemsSource = CheckoutItems;
    }
	

	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
		

	}
}
