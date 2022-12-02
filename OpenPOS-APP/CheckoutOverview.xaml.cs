using CommunityToolkit.Maui.Views;
using OpenPOS_APP.Models;
using OpenPOS_APP.Resources.Controls.PopUps;
using OpenPOS_APP.Services.Models;
using OpenPOS_APP.Settings;
using System.Diagnostics;
using System.Globalization;

namespace OpenPOS_APP;

public partial class CheckoutOverview : ContentPage
{
    public Dictionary<Product, int> CheckoutItems { get; set; }

    public double TotalPrice;

    private double _tip = 0;

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
            TotalPrice += (products.ElementAt(i).Key.Price * products.ElementAt(i).Value);
        }
        string value = String.Format(((Math.Round(TotalPrice) == TotalPrice) ? "{0:0}" : "{0:0.00}"), TotalPrice);

        TotalPriceLabel.Text = $"€{value}";

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
               int totalInCents = (int)Math.Round(TotalPrice * 100);
               int tipInCents = (int)Math.Round(_tip * 100);
               int total = totalInCents + tipInCents;
               int splitamount = totalInCents / count;
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
      int totalInCents = (int)Math.Round(TotalPrice * 100);
      int tipInCents = (int)Math.Round(_tip * 100);
      int total = totalInCents + tipInCents;
      Transaction transaction = TikkiePayementService.CreatePaymentRequest(total, ApplicationSettings.CurrentBill.Id, $"OpenPOS Tikkie Payment: {ApplicationSettings.CurrentBill.Id}");
      PaymentPage.SetTransaction(transaction, 1);
      await Shell.Current.GoToAsync(nameof(PaymentPage));
   }

   private void OnClickedAddaTip(object sender, EventArgs args)
   {
      TipPopUp popUp = new TipPopUp(TotalPrice, this);
      this.ShowPopup(popUp);

   }

   public void OnTipAdded(object sender, EventArgs args)
   {
      if (sender is TipPopUp)
      {
         TipPopUp pop = (TipPopUp)sender;
         _tip = pop.tip;
      } else if (sender is InputCustomTipPopUp)
      {
         InputCustomTipPopUp pop = (InputCustomTipPopUp)sender;
         _tip = pop.tip;
      }
   }

   public async void OnEditTip(object sender, EventArgs args)
   {
      var result = await DisplayAlert("Editing Tip", "Are you sure you want to edit the tip you added?", "Yes", "No");
      if (result == true)
      {
         Debug.WriteLine("Edit2");
      } else if (result == false)
      {
         Debug.WriteLine("Edit1");
      }
   }

   protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
	}
}
