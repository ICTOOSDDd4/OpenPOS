﻿using CommunityToolkit.Maui.Views;
using Microsoft.Maui;
using OpenPOS_APP.Resources.Controls.PopUps;
using OpenPOS_APP.Settings;
using System.Diagnostics;
using System.Globalization;
using OpenPOS_APP.Services;
using System.Linq;

namespace OpenPOS_APP;

public partial class CheckoutOverview : ContentPage
{ 
    private Dictionary<Product, int> CheckoutItems { get; set; }

    private double TotalPrice;

    private double _tip;


    public static Dictionary<Product,int> GetCheckoutItems()
    {
        return ApplicationSettings.CheckoutList;
    }

    public CheckoutOverview()
	 {
        InitializeComponent();
	  AddToCheckOut(ApplicationSettings.CheckoutList);
        Header.currentPage = this;
    }


    public void AddToCheckOut(Dictionary<Product, int> products)
    {
        CheckoutItems = products;
        CheckoutListView.ItemsSource = CheckoutItems.Keys;
        for (int i = 0; i < products.Count; i++)
        {
            TotalPrice += (products.ElementAt(i).Key.Price * products.ElementAt(i).Value);
        }
        string value = String.Format(((Math.Round(TotalPrice + _tip) == TotalPrice + _tip) ? "{0:0}" : "{0:0.00}"), TotalPrice + _tip);

        TotalPriceLabel.Text = $"Total: €{value}";

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
               Transaction transaction = TikkiePaymentService.CreatePaymentRequest(splitamount, ApplicationSettings.CurrentBill.Id, $"OpenPOS Tikkie Payment: {ApplicationSettings.CurrentBill.Id}");
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
      Transaction transaction = TikkiePaymentService.CreatePaymentRequest(total, ApplicationSettings.CurrentBill.Id, $"OpenPOS Tikkie Payment: {ApplicationSettings.CurrentBill.Id}");
      PaymentPage.SetTransaction(transaction, 1);
      await Shell.Current.GoToAsync(nameof(PaymentPage));
      
   }

   private void OnClickedAddATip(object sender, EventArgs args)
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
         TipButton.Clicked -= OnClickedAddATip;
         TipButton.Clicked += OnEditTip;
         AddTipOnButton();

      } else if (sender is InputCustomTipPopUp)
      {
         InputCustomTipPopUp pop = (InputCustomTipPopUp)sender;
         _tip = pop.tip;
         TipButton.Clicked -= OnClickedAddATip;
         TipButton.Clicked += OnEditTip;
         AddTipOnButton();
      }
   }

   public async void OnEditTip(object sender, EventArgs args)
   {
      string[] options = { "Change tip", "Remove Tip" };
      var result = await DisplayActionSheet("Edit your tip", null , "Cancel", options);
      if (result == "Change tip")
      {
         OnClickedAddATip(this, args);
      } else if (result == "Remove Tip")
      {
         _tip = 0;
         TipButton.Text = "Add a tip";
         TipButton.Clicked -= OnEditTip;
         TipButton.Clicked += OnClickedAddATip;
         string totalValue = String.Format(((Math.Round(TotalPrice + _tip) == TotalPrice + _tip) ? "{0:0}" : "{0:0.00}"), TotalPrice + _tip);
         TotalPriceLabel.Text = $"€{totalValue}";
         Debug.WriteLine("Remove");
      }
   }

   private void AddTipOnButton()
   {
      string tipValue = String.Format(((Math.Round(_tip) == _tip) ? "{0:0}" : "{0:0.00}"), _tip);

      TipButton.Text = $"Tip: €{tipValue}";

      string totalValue = String.Format(((Math.Round(TotalPrice + _tip) == TotalPrice + _tip) ? "{0:0}" : "{0:0.00}"), TotalPrice + _tip);

      TotalPriceLabel.Text = $"€{totalValue}";
   }
}
