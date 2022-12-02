using CommunityToolkit.Maui.Views;
using OpenPOS_APP.Models;
using OpenPOS_APP.Resources.Controls.PopUps;
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

	public async void OnClickedSplitPay(object sender, EventArgs args)
    {
      bool loop = true;

      while(loop)
      {
         var result = await DisplayPromptAsync("Going Dutch!", "With how many people do you wanna split the bill?", maxLength: 2, placeholder: "Enter the number of people.", keyboard: Keyboard.Numeric);
         if (int.TryParse(result, out var count))
         {
            if (!int.IsNegative(count))
            {
               int splitamount = TotalPrice / count;
               Transaction transaction = TikkiePayementService.CreatePaymentRequest(splitamount, ApplicationSettings.CurrentBill.Id, $"OpenPOS Tikkie Payment: {ApplicationSettings.CurrentBill.Id}");
               if (transaction.Url != null)
               {
                  PaymentPage.SetTransaction(transaction, count);
                  loop = false;
                  await Shell.Current.GoToAsync(nameof(PaymentPage));
                  continue;
               }
               else throw new Exception($"Payment Error: {splitamount} isn't compatible with the API.");
            }
            await DisplayAlert("Oops", "You can't split a bill with a negative amount of people!", "Try Again");
            continue;
         }
         else if (result == null)
         {
            loop = false;
            continue;
         }
         await DisplayAlert("Oops", "You can only input a number.", "Try Again");
         continue;
      }
   }

   private async void OnClickedPay(object sender, EventArgs args)
   {
      Transaction transaction = TikkiePayementService.CreatePaymentRequest(TotalPrice, ApplicationSettings.CurrentBill.Id, $"OpenPOS Tikkie Payment: {ApplicationSettings.CurrentBill.Id}");
      PaymentPage.SetTransaction(transaction, 1);
      await Shell.Current.GoToAsync(nameof(PaymentPage));
   }

   private async void OnClickedAddaTip(object sender, EventArgs args)
   {
      TipPopUp popUp = new TipPopUp(TotalPrice);
      this.ShowPopup(popUp);

   }

   protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
	}
}
