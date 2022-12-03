using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls.Platform;
using OpenPOS_APP.Services.Models;
using OpenPOS_APP.Settings;
using System.Globalization;

namespace OpenPOS_APP.Resources.Controls.PopUps;

public partial class TipPopUp : Popup
{
   private double _price;
   private CheckoutOverview _overview;
   public double tip;
	public TipPopUp(double price, CheckoutOverview sender)
	{
		InitializeComponent();
      _price = price;
      _overview = sender;
      string value = String.Format(((Math.Round(price) == price) ? "{0:0}" : "{0:0.00}"), price);
      MoneyLabel.Text = $"New Total: €{value}";
   }

   private void CustomTip_Button_Clicked(object sender, EventArgs e)
   {
      InputCustomTipPopUp pop = new();
      pop.TipAdded += _overview.OnTipAdded;
      _overview.ShowPopup(pop);
      Close();
   }

   private void Closing_Button_Clicked(object sender, EventArgs e)
   {
      Close();
   }

   private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
   {
      double t = e.NewValue / 100;
      double factor = 1 + t;
      double newPrice = _price * factor;
      newPrice = Math.Round(newPrice, 2);
      tip = newPrice - _price;
      string value = String.Format(((Math.Round(newPrice) == newPrice) ? "{0:0}" : "{0:0.00}"), newPrice);
      MoneyLabel.Text = $"New Total: €{value}";
   }

   private void Add_Button_Clicked(object sender, EventArgs e)
   {
      if (tip != 0)
      {
         _overview.OnTipAdded(this, e);
         Close();

      } else { Change_Error_Label(true, "The tip amount can't be zero."); }
      
   }

   private void Change_Error_Label(bool active, string text)
   {
      ErrorDisplayLabel.IsVisible = active;
      ErrorDisplayLabel.Text = text;
   }
}