namespace OpenPOS_APP.Resources.Controls;

public partial class Header : StackLayout
{
   public bool Showing { get; set; }

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

   }

   private void OnSearchTextChanged(object sender, EventArgs e) { }
}