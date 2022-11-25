using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Models;
using Windows.ApplicationModel.Activation;

namespace OpenPOS_APP.Resources.Controls;

public partial class Header : StackLayout
{
   public bool Showing { get; set; }

    public delegate void OnSearchEventHandler(object source, EventArgs args, List<Product> products);
    public static event OnSearchEventHandler Searched;

    private static readonly BindableProperty headerProperty = BindableProperty.Create(
        propertyName: nameof(Showing),
        returnType: typeof(bool),
        defaultValue: true,
        declaringType: typeof(Header),
        defaultBindingMode: BindingMode.OneWay);

    public Header()
	{
		InitializeComponent();
	}

   private void OnSearch(object sender, EventArgs e)
   {
        var result = ProductService.GetAllByFilter(((SearchBar)sender).Text);
        throw new Exception(result.ToString());
        Searched(this, EventArgs.Empty, result);
   }

   private void OnSearchTextChanged(object sender, EventArgs e) { }
}