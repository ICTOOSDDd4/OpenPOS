using Microsoft.Maui.Controls.Platform;

namespace OpenPOS_APP;

public partial class ProductView : ContentView
{
   public int Amount { get; set; }


   public ProductView()
   {
      InitializeComponent();
      MainVerticalLayout.Shadow = new Shadow
      {
         Offset = new Point(10, 10),
         //Radius = 10,
         Brush = Brush.Black,
         Opacity = 0.12f,
      };
   }

   public void SetPoductValues(string name, string desc, string imagePath)
   {
      ProductName.Text = name;
      ProductInfo.Text = desc;
      ProductImage.Source = imagePath;
   }

   private void OnClickedToevoegen(object sender, EventArgs e)
   {
      Amount++;
      AmountCount.Text = Amount.ToString();
   }

   private void OnClickedVerwijderen(object sender, EventArgs e)
   {
      Amount--;
      AmountCount.Text = Amount.ToString();
   }

    private void AmountCount_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      if (AmountLabel != null)
      {
         if (Amount > 0)
         {
            AmountLabel.IsVisible = true;
            DeleteButton.IsVisible = true;
         }
         else
         {
            AmountLabel.IsVisible = false;
            DeleteButton.IsVisible = false;
         }
      }
      
    }
}