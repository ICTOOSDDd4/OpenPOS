using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Models;
using OpenPOS_APP.Settings;
using System.Globalization;


namespace OpenPOS_APP;

public partial class CheckoutOverview : ContentPage
{
    public Dictionary<Product, int> CheckoutItems { get; set; }

    public int TotalPrice;

    public static Dictionary<Product,int> GetCheckoutItems()
    {
        return ApplicationSettings.CheckoutList;
    }

    public CheckoutOverview()
	{
        InitializeComponent();

        
		AddToCheckOut(ApplicationSettings.CheckoutList);

	}


    public void AddToCheckOut(Dictionary<Product, int> products)
    {
        CheckoutItems = products;
        CheckoutListView.ItemsSource = CheckoutItems.Keys;
        for (int i = 0; i < products.Count; i++)
        {
            TotalPrice += (int)(products.ElementAt(i).Key.Price * products.ElementAt(i).Value);
        }
        TotalPriceLabel.Text = "€" + TotalPrice.ToString(CultureInfo.InvariantCulture);
    }

	public async void OnClicked(Object obj, EventArgs args)
    {
        await DisplayAlert("Test", "AAAAAH", "Ok");
    }
	

	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
	}
}
