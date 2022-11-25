using OpenPOS_APP.Models;
using System.Reflection;

namespace OpenPOS_APP;

public partial class ProductView : ContentView
{
   public int Amount { get; set; }
   public EventHandler ClickedMoreInfo;
   private MenuPage _menuPage;
   private Product _product;
   private ResourceDictionary _appColors = new();

   public ProductView()
   {
      InitializeComponent();
      _appColors.SetAndLoadSource(new Uri("Resources/Styles/Colors.xaml", UriKind.RelativeOrAbsolute), "Resources/Styles/Colors.xaml", this.GetType().GetTypeInfo().Assembly, null);
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
      ProductPrice.Text = $"€{ product.Price }";
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
      ClickedMoreInfo?.Invoke(this, e);
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
         if (_appColors.TryGetValue("DeleteRed", out var color))
         {
            DeleteButton.BackgroundColor = (Color)color;
         }
      }
      else
      {
         DeleteButton.IsEnabled = false;
         if (_appColors.TryGetValue("Gray100", out var color))
         {
            DeleteButton.BackgroundColor = (Color)color;
         }
      }
   }

}