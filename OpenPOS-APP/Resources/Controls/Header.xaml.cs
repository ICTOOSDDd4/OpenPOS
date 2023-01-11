using System.Diagnostics;
using OpenPOS_Settings;

namespace OpenPOS_APP.Resources.Controls;

public partial class Header : StackLayout
{
    public static event EventHandler Searched;
    public ContentPage CurrentPage { get; set; }

    public Header()
	{
		InitializeComponent();
      TableNumber.Text = $"Table: { ApplicationSettings.TableNumber }";

   }

   private void OnSearch(object sender, EventArgs e)
   {
      Debug.WriteLine("Invoked Search");
      if (Searched != null) Searched.Invoke(sender, e);
   }

   private void OnSearchTextChanged(object sender, EventArgs e) { } // This is required for when live search will be added to the app


   private async void OnClickedLogout(object sender, EventArgs e)
   {
      bool alert = await CurrentPage.DisplayAlert("Loging out", "Are you sure you want to log out?", "Yes", "No");
      if (alert)
      {
         if (ApplicationSettings.CheckoutList.Count == 0)
         {
            await Shell.Current.GoToAsync(nameof(GoodbyePage));
            return;
         }
         await CurrentPage.DisplayAlert("Oops", "You will have to pay the current bill first to be able to log out!", "Understood");
      }
   }
   private async void OnClickedCard(object sender, EventArgs e) 
   {
      await Shell.Current.GoToAsync(nameof(CheckoutOverview));
   }
}