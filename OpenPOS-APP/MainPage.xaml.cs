
using Microsoft.Maui.Controls;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Models;
using OpenPOS_APP.Settings;
using OpenPOS_APP.Views.Onboarding;
using System.Reflection;
namespace OpenPOS_APP;

public partial class MainPage : ContentPage
{
	private ResourceDictionary _appColors = new();
	public MainPage()
	{
		InitializeComponent();
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

   private void ActivateButton(bool active)
   {
      if (active)
      {
         MainLoginButton.IsEnabled = true;
         if (_appColors.TryGetValue("OpenPos-Yellow", out var color))
         {
            MainLoginButton.BackgroundColor = (Color)color;
         }
      }
      else
      {
         MainLoginButton.IsEnabled = false;
         if (_appColors.TryGetValue("Gray100", out var color))
         {
            MainLoginButton.BackgroundColor = (Color)color;
         }
      }
   }

}

