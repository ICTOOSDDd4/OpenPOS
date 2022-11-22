
﻿using System.Reflection;
 using OpenPOS_APP.Services.Models;
namespace OpenPOS_APP;

public partial class MainPage : ContentPage
{
	private string _username;
	private string _password;
	private ResourceDictionary AppColors = new ResourceDictionary();
	public MainPage()
	{
		InitializeComponent();
		AppColors.SetAndLoadSource(new Uri("Resources/Styles/Colors.xaml", UriKind.RelativeOrAbsolute), "Resources/Styles/Colors.xaml", this.GetType().GetTypeInfo().Assembly, null );
	}
	
	private async void CreateNewAccount_Tapped(object sender, EventArgs e)
	{
		// Navigation.PushAsync();
		await DisplayAlert("Test", "Creating account...", "OK");

	}
	
	private void OnTextFilledUsername(object sender, TextChangedEventArgs e)
	{
		_username = e.NewTextValue;
		if (string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_username))
		{
			MainLoginButton.IsEnabled = false;
         if (AppColors.TryGetValue("Gray100", out var color))
         {
            MainLoginButton.BackgroundColor = (Color)color;
         }
         EmailEntry.Placeholder = "Please enter your email";
		} 
		else
		{
			MainLoginButton.IsEnabled = true;
			if (AppColors.TryGetValue("OpenPos-Yellow", out var color))
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
         if (AppColors.TryGetValue("Gray100", out var color))
         {
            MainLoginButton.BackgroundColor = (Color)color;
         }
         PasswordEntry.Placeholder = "Please enter your password";
		}
		else
		{
			MainLoginButton.IsEnabled = true;
			if (AppColors.TryGetValue("OpenPos-Yellow", out var color))
			{
				MainLoginButton.BackgroundColor = (Color)color;
			}
		}
	}

	private async void OnLoginButtonClicked(object sender, EventArgs e)
	{
		//TODO: Login auth with DB
		
		await DisplayAlert("Test", "Logging in...", "OK");


	}

    
}

