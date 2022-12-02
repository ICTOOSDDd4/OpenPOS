using CommunityToolkit.Maui.Views;

namespace OpenPOS_APP.Resources.Controls.PopUps;

public partial class TipPopUp : Popup
{
   private int _price;
	public TipPopUp(int price)
	{
		InitializeComponent();
      _price = price;
      MoneyLabel.Text = $"New Total: €{price}";
   }

   private void CustomTip_Button_Clicked(object sender, EventArgs e)
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
      decimal newPrice = (decimal)(_price * factor);
      newPrice = Math.Round(newPrice, 2);

      MoneyLabel.Text = $"New Total: €{newPrice}";
    }
}