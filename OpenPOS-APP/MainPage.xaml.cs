
using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Models;
using OpenPOS_APP.Settings;
using System.Reflection;
namespace OpenPOS_APP;

public partial class MainPage : ContentPage
{
	private string _username;
	private string _password;
	private ResourceDictionary _appColors = new();
	public MainPage()
	{
		InitializeComponent();
		_appColors.SetAndLoadSource(new Uri("Resources/Styles/Colors.xaml", UriKind.RelativeOrAbsolute), "Resources/Styles/Colors.xaml", this.GetType().GetTypeInfo().Assembly, null );
	}
	private void OnTextFilledUsername(object sender, TextChangedEventArgs e)
	{
		_username = e.NewTextValue;
		if (string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_username))
		{
			MainLoginButton.IsEnabled = false;
         if (_appColors.TryGetValue("Gray100", out var color))
         {
            MainLoginButton.BackgroundColor = (Color)color;
         }
         EmailEntry.Placeholder = "Please enter your email";
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

	private void OnTextFilledPassword(object sender, TextChangedEventArgs e)
	{
		_password = e.NewTextValue;
		if (string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_username))
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
			await Shell.Current.GoToAsync(nameof(TafelKeuzeScherm));

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

    
}

