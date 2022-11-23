using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Models;

namespace OpenPOS_APP;

public partial class CheckoutOverview : ContentPage
{
	public CheckoutOverview()
	{
		InitializeComponent();

        List<Product> CheckoutItems = new List<Product>();
        for (int i = 0; i < 10; i++)
        {
            CheckoutItems.Add(new Product()
            {
                Name = "Product " + i,
                Price = i + 0.99,
                Description = "Dit is product " + i
            });
        }
        
        CheckoutListView.ItemsSource = CheckoutItems;

	}
	

	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
		

	}
}
