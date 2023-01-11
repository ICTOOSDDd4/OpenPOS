using CommunityToolkit.Maui.Views;
using OpenPOS_Models;

namespace OpenPOS_APP.Resources.Controls.PopUps;

public partial class ProductInfoPopUp : Popup
{
	public ProductInfoPopUp()
	{
		InitializeComponent();
	}

    public void SetProduct(Product product)
    {
        Image.Source = product.Imagepath;
        ProductName.Text = product.Name;
        ProductDescription.Text = product.Description;
        ProductAllergies.Text = "Allergies: " + product.Allergies;
    }

}