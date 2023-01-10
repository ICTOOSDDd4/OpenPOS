using System.Reflection;
using OpenPOS_Models;
using OpenPOS_Settings;
using OpenPOS_Settings.EventArgsClasses;
using Brush = Microsoft.Maui.Controls.Brush;
using Shadow = Microsoft.Maui.Controls.Shadow;

namespace OpenPOS_APP.Resources.Controls;

public partial class ProductView : ContentView
{
    public int Amount { get; set; }
    public event EventHandler<InfoButtonEventArgs> ClickedMoreInfo;
    private MenuPage _menuPage;
    private Product _product;
    private readonly ResourceDictionary _appColors = new();

    public ProductView()
    {
        InitializeComponent();
        MainVerticalLayout.Shadow = new Shadow
        {
            Offset = new Point(5, 5),
            Brush = Brush.Black,
            Opacity = 0.12f,
        };
        _appColors.SetAndLoadSource(new Uri("Resources/Styles/Colors.xaml", UriKind.RelativeOrAbsolute), "Resources/Styles/Colors.xaml", this.GetType().GetTypeInfo().Assembly, null);
    }

    public void SetProductValues(MenuPage page, Product product)
    {
        _menuPage = page;
        _product = product;
        ProductName.Text = product.Name;
        ProductInfo.Text = product.Description;
        string value = String.Format(((Math.Round(product.Price) == product.Price) ? "{0:0}" : "{0:0.00}"), product.Price);
        ProductPrice.Text = $"€ {value}";

        if (product.Imagepath != null)
        {
            ProductImage.Source = ImageSource.FromUri(new Uri(product.Imagepath));
        }
        if (_menuPage.SelectedProducts.ContainsKey(_product.Id))
        {
            Amount = _menuPage.SelectedProducts[_product.Id];
            AmountCount.Text = Amount.ToString();
        }
    }

    private void OnClickedAdd(object sender, EventArgs e)
    {
        Amount++;
        AmountCount.Text = Amount.ToString();

        // Add to current selected products
        if (_menuPage.SelectedProducts.ContainsKey(_product.Id))
        {
            _menuPage.SelectedProducts[_product.Id] = Amount;
        }
        else
        {
            _menuPage.SelectedProducts.Add(_product.Id, Amount);
        }

        // Add to over checkoutList
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
        ClickedMoreInfo?.Invoke(this, new InfoButtonEventArgs(_product));
    }

    private void OnClickedDelete(object sender, EventArgs e)
    {
        Amount--;
        AmountCount.Text = Amount.ToString();

        // Remove from currently selected products
        if (_menuPage.SelectedProducts.ContainsKey(_product.Id))
        {
            if (_menuPage.SelectedProducts[_product.Id] > 1)
            {
                _menuPage.SelectedProducts[_product.Id]--;

            }
            else
            {
                _menuPage.SelectedProducts.Remove(_product.Id);
            }
        }

        // Remove from overall checkoutList
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
