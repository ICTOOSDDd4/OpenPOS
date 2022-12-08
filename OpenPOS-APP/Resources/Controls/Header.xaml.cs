using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Models;
using System.Diagnostics;
using OpenPOS_APP.Settings;
using System.Diagnostics;

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
    public ContentPage currentPage { get; set; }

    public Header()
	{
		InitializeComponent();
      TableNumber.Text = $"Table: { ApplicationSettings.TableNumber }";

   }

   private void OnSearch(object sender, EventArgs e)
   {
        Debug.WriteLine("Invoked Search");
      Searched.Invoke(sender, e);
   }

   private void OnSearchTextChanged(object sender, EventArgs e) { }


   private async void OnClickedLogout(object sender, EventArgs e)
   {
      bool alert = await currentPage.DisplayAlert("Loging out", "Are you sure you want to log out?", "Yes", "No");
      if (alert)
      {
         if (ApplicationSettings.CheckoutList.Count == 0)
         {
            await Shell.Current.GoToAsync(nameof(GoodbyePage));
            return;
         }
         await currentPage.DisplayAlert("Oops", "You will have to pay the current bill first to be able to log out!", "Understood");
         return;
      }
   }
   private async void OnClickedCard(object sender, EventArgs e) 
   {
      await Shell.Current.GoToAsync(nameof(CheckoutOverview));
   }
}