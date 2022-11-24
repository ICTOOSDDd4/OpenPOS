using Microsoft.Maui.Controls.Platform;
using OpenPOS_APP.Models;

namespace OpenPOS_APP;

public partial class ProductView : ContentView
{
   public int Amount { get; set; }
   private MenuPage _menuPage;
   private Product _product;

   public ProductView()
   {
      InitializeComponent();
      MainVerticalLayout.Shadow = new Shadow
      {
         Offset = new Point(10, 10),
         Brush = Brush.Black,
         Opacity = 0.12f,
      };
   }

   public void SetProductValues(MenuPage page, Product product)
   {
      _menuPage = page;
      _product = product;
      
      //TODO: Add price to product
      
      ProductName.Text = product.Name;
      ProductInfo.Text = product.Price + ", " + product.Description;
      // ProductImage.Source = imagePath; --Needs to be implemented in DB
   }

   private void OnClickedToevoegen(object sender, EventArgs e)
   {
      Amount++;
      AmountCount.Text = Amount.ToString();
      
      _menuPage.SelectedProducts.Add(_product);
   }

   private void OnClickedInfo(object sender, EventArgs e)
   {

   }

   private void OnClickedVerwijderen(object sender, EventArgs e)
   {
      Amount--;
      AmountCount.Text = Amount.ToString();
      
      _menuPage.SelectedProducts.Remove(_product);
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