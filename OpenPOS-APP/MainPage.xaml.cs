
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
         PasswordEntry.Placeholder = "Please enter your password";
		}
		else
		{
			MainLoginButton.IsEnabled = true;
			if (_appColors.TryGetValue("OpenPos-Yellow", out var color))
			{
				MainLoginButton.BackgroundColor = (Color)color;
			}
		}
	}

	private async void OnLoginButtonClicked(object sender, EventArgs e)
	{
		//TODO: Login auth with DB
		//await DisplayAlert("Test", "Logging in...", "OK");
      if (UserAuth(_username, _password)) 
		{
			//TODO: ROUING
		} else { await DisplayAlert("Oops", "You got it wrong", "Try again"); }
		
	}
	
	private async void CreateNewAccount_Tapped(object sender, EventArgs e)
	{
		// Navigation.PushAsync();
		await DisplayAlert("Test", "Creating account...", "OK");

	}

	private bool UserAuth(string username, string password)
	{
		User user = UserService.Authenticate(_username, _password);

		if (user != null)
		{
			ApplicationSettings.LoggedinUser = user;
			return true;
		} else { return false; }
	}
    private void OnSearch(object sender, TextChangedEventArgs e)
    {
    }
}

