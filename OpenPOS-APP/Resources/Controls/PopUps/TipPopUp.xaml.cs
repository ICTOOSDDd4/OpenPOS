using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls.Platform;
using OpenPOS_APP.Services.Models;
using OpenPOS_APP.Settings;

namespace OpenPOS_APP.Resources.Controls.PopUps;

public partial class TipPopUp : Popup
{
   private double _price;
	public TipPopUp(double price)
	{
		InitializeComponent();
      _price = price;
      MoneyLabel.Text = $"New Total: €{price}";
   }

   private async void CustomTip_Button_Clicked(object sender, EventArgs e)
   {
      
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

      MoneyLabel.Text = $"New Total: €{newPrice}";
    }
}