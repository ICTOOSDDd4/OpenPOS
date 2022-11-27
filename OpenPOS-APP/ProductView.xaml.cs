using OpenPOS_APP.Models;
using OpenPOS_APP.Settings;

namespace OpenPOS_APP;

public partial class ProductView : ContentView
{
   public int Amount { get; set; }
   public EventHandler ClickedMoreInfo;
   private MenuPage _menuPage;
   private Product _product;

   public ProductView()
   {
      InitializeComponent();
      MainVerticalLayout.Shadow = new Shadow
      {
         Offset = new Point(5, 5),
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
      ProductInfo.Text = product.Description;
      ProductPrice.Text = $"€ { product.Price }";
      // ProductImage.Source = imagePath; --Needs to be implemented in DB
   }

   private void OnClickedToevoegen(object sender, EventArgs e)
   {
      Amount++;
      AmountCount.Text = Amount.ToString();
      
      // Add to current selected products
      if (_menuPage.SelectedProducts.ContainsKey(_product))
      {
         _menuPage.SelectedProducts[_product] = Amount;
      }
      else
      {
         _menuPage.SelectedProducts.Add(_product, Amount);
      }

      // Add to over checkoutlist
      if (ApplicationSettings.CheckoutList.ContainsKey(_product))
      {
         ApplicationSettings.CheckoutList[_product]++;
      } else
      {
         ApplicationSettings.CheckoutList.Add(_product, Amount);
      }

   }

   private void OnClickedInfo(object sender, EventArgs e)
   {
      ClickedMoreInfo?.Invoke(this, e);
   }

   private void OnClickedVerwijderen(object sender, EventArgs e)
   {
      Amount--;
      AmountCount.Text = Amount.ToString();

      // Remove from currently selected products
      if (_menuPage.SelectedProducts.ContainsKey(_product))
      {
         if (_menuPage.SelectedProducts[_product] > 1)
         {
            _menuPage.SelectedProducts[_product]--;

         }
         else
         {
            _menuPage.SelectedProducts.Remove(_product);
         }
      }

      // Remove from overall checkoutlist
      if (ApplicationSettings.CheckoutList.ContainsKey(_product))
      {
         if(ApplicationSettings.CheckoutList[_product] > 1)
         {
            ApplicationSettings.CheckoutList[_product]--;

         } else
         {
            ApplicationSettings.CheckoutList.Remove(_product);
         }
      }
   }

    private void AmountCount_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      if (AmountLabel != null)
      {
         if (Amount > 0)
         {
            AmountLabel.IsVisible = true;
            ActivateButton(true);
         }
         else
         {
            AmountLabel.IsVisible = false;
            ActivateButton(false);
         }
      }
      
    }
   private void ActivateButton(bool active)
   {
      if (active)
      {
         DeleteButton.IsEnabled = true;
         if (ApplicationSettings.UIElements.AppColors.TryGetValue("DeleteRed", out var color))
         {
            DeleteButton.BackgroundColor = (Color)color;
         }
      }
      else
      {
         DeleteButton.IsEnabled = false;
         if (ApplicationSettings.UIElements.AppColors.TryGetValue("Gray100", out var color))
         {
            DeleteButton.BackgroundColor = (Color)color;
         }
      }
   }

}