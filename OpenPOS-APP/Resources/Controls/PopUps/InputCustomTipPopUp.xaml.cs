using CommunityToolkit.Maui.Views;

namespace OpenPOS_APP.Resources.Controls.PopUps;

public partial class InputCustomTipPopUp : Popup
{
   public event EventHandler TipAdded;
   public double Tip;
   public InputCustomTipPopUp()
	{
		InitializeComponent();
	}

    private void Add_Button_Clicked(object sender, EventArgs e)
    {
      string entryString = TipAmount.Text;
      if (!entryString.Contains('.'))
      {
         if (double.TryParse(entryString.Trim(), out double value))
         {
            if (!double.IsNegative(value))
            {
               if (value != 0)
               {
                  Tip = value;
                  if (TipAdded != null)
                  {
                     TipAdded.Invoke(this, e);
                     Close();
                  } else { Change_Error_Label(true, "A system error occured, please contact a staff member."); }
               }
               else { Change_Error_Label(true, "You can't enter zero."); }
            }
            else { Change_Error_Label(true, "You can't enter a negative number."); }
         }
         else { Change_Error_Label(true, "You need to give a valid number"); }
      } else { Change_Error_Label(true, "You need to use a comma instead of a dot."); }
      
   }

   private void Closing_Button_Clicked(object sender, EventArgs e)
   {
      Close();
   }

   private void Change_Error_Label(bool active, string text) // A extra method to remove redundant code
   {
      ErrorDisplayLabel.IsVisible = active;
      ErrorDisplayLabel.Text = text;      
   }
}