
using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Models;
using System.Diagnostics;
using OpenPOS_APP.Settings;

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
      TableNumber.Text = $"Tafel: { ApplicationSettings.TableNumber }";

   }

   private void OnSearch(object sender, EventArgs e)
   {
        Debug.WriteLine("Invoked Search");
      Searched.Invoke(sender, e);
   }

   private void OnSearchTextChanged(object sender, EventArgs e) { }


   private async void OnClickedAccount(object sender, EventArgs e)
   {
      await Shell.Current.DisplayAlert("Work In Progress", "This feature will be released soon, thank you for your patience.", "Understood");
   }
   private async void OnClickedCard(object sender, EventArgs e) 
   {
      await Shell.Current.GoToAsync(nameof(CheckoutOverview));
   }
}