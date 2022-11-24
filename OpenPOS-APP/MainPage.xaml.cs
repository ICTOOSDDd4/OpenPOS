
using Microsoft.Maui.Controls;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Models;
using OpenPOS_APP.Settings;
using OpenPOS_APP.Views.Onboarding;
using System.Reflection;
using Windows.UI.ViewManagement;

namespace OpenPOS_APP;

public partial class MainPage : ContentPage
{
	private ResourceDictionary _appColors = new();
	public MainPage()
	{
      InitializeComponent();
      DeviceDisplay.Current.MainDisplayInfo
      _appColors.SetAndLoadSource(new Uri("Resources/Styles/Colors.xaml", UriKind.RelativeOrAbsolute), "Resources/Styles/Colors.xaml", this.GetType().GetTypeInfo().Assembly, null );
      OnIconLoaded();
   }

   private async void OnIconLoaded()
   {
      await openposicon.RelRotateTo(360, 4000);
   }

	private async void OnLoginButtonClicked(object sender, EventArgs e)
	{
      await Shell.Current.GoToAsync(nameof(LoginScreen));
   }
	
	private async void CreateNewAccount_Tapped(object sender, EventArgs e)
	{
		// Navigation.PushAsync();
		await DisplayAlert("Test", "Creating account...", "OK");

	}
    private void OnSearch(object sender, TextChangedEventArgs e)
    {
    }
}

