using CommunityToolkit.Maui.Views;

namespace OpenPOS_APP.Resources.Controls.PopUps;

public partial class InputCustomTipPopUp : Popup
{
   public event EventHandler TipAdded;
   public double tip;
   public InputCustomTipPopUp()
	{
		InitializeComponent();
	}

    private void Add_Button_Clicked(object sender, EventArgs e)
    {
      string entryString = Add_Button.Text;
      if (double.TryParse(entryString.ToString().Trim(), out double value))
      {
         if (!double.IsNegative(value))
         {
            TipAdded.Invoke(this, e);
            tip = value;
            Close();
         } else { Change_Error_Label(true, "You can't enter a negative number.");  }
      } else { Change_Error_Label(true, "You need to give a valid number"); }
   }

   private void Closing_Button_Clicked(object sender, EventArgs e)
   {
      Close();
   }

   private void Change_Error_Label(bool active, string text)
   {
      ErrorDisplayLabel.IsVisible = active;
      ErrorDisplayLabel.Text = text;
      
   }
}