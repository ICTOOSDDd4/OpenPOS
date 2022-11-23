using OpenPOS_APP.Models;

namespace OpenPOS_APP;

public partial class CheckoutOverview : ContentPage
{
	public CheckoutOverview()
	{
		InitializeComponent();
		
		List<Product> CheckoutItems = new List<Product>();
		
		CheckoutItems.Add(new Product(1, "Lasagne bolognese", 15, "Van authentiek Italiaans recept"));
		CheckoutItems.Add(new Product(1, "Lasagne Kaasje", 16, "Van authentiek Italiaans recept"));
		CheckoutItems.Add(new Product(1, "Lasagne met bananen", 222, "Van authentiek Italiaans recept"));
        CheckoutItems.Add(new Product(1, "Lasagne bolognese", 15, "Van authentiek Italiaans recept"));
        CheckoutItems.Add(new Product(1, "Lasagne Kaasje", 16, "Van authentiek Italiaans recept"));
        CheckoutItems.Add(new Product(1, "Lasagne met bananen", 222, "Van authentiek Italiaans recept"));
        CheckoutItems.Add(new Product(1, "Lasagne bolognese", 15, "Van authentiek Italiaans recept"));
        CheckoutItems.Add(new Product(1, "Lasagne Kaasje", 16, "Van authentiek Italiaans recept"));
        CheckoutItems.Add(new Product(1, "Lasagne met bananen", 222, "Van authentiek Italiaans recept"));


        CheckoutListView.ItemsSource = CheckoutItems;

	}
	

	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
		

	}
}
