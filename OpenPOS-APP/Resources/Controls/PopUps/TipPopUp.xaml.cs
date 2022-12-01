using CommunityToolkit.Maui.Views;

namespace OpenPOS_APP.Resources.Controls.PopUps;

public partial class TipPopUp : Popup
{
	public TipPopUp()
	{
		InitializeComponent();
	}
   private void Button_Clicked(object sender, EventArgs e)
   {
      Close();
   }
}