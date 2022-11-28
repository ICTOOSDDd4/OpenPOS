using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Models;
using Windows.ApplicationModel.Activation;

namespace OpenPOS_APP.Resources.Controls;

public partial class Header : StackLayout
{
   public bool Showing { get; set; }

    public static event EventHandler Searched;

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
      Searched.Invoke(sender, e);
   }

   private void OnSearchTextChanged(object sender, EventArgs e) { }
}