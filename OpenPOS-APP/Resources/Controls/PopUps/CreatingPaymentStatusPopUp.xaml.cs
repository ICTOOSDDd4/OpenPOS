using CommunityToolkit.Maui.Views;

namespace OpenPOS_APP.Resources.Controls.PopUps;

public partial class CreatingPaymentStatusPopUp : Popup
{
	public CreatingPaymentStatusPopUp()
	{
		InitializeComponent();
	}

	public void ChangeLabel(string label)
	{
      StatusLabel.Text = label;
   }
}