using Microsoft.Extensions.Configuration;
namespace OpenPOS_APP;

public partial class MainPage : ContentPage
{
	private string _username;
	private string _password;
	private ResourceDictionary AppColors = new ResourceDictionary();
	public MainPage(IConfiguration config)
	{
		InitializeComponent();
		AppColors.Add("Resources/Styles/Colors.xaml", typeof(ResourceDictionary));
	}

	private void OnTextFilledUsername(object sender, EventArgs e)
	{
		_username = nameof(UsernameEntry.Text);
		if (string.IsNullOrEmpty(_username))
		{
			MainLoginButton.IsEnabled = false;
			UsernameEntry.Placeholder = "Please enter your username";
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

	private void OnTextFilledPassword(object sender, EventArgs e)
	{
		_password = nameof(PasswordEntry.Text);
		if (string.IsNullOrEmpty(_password))
		{
			MainLoginButton.IsEnabled = false;
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

	private void OnLoginButtonClicked(object sender, EventArgs e)
	{
		//TODO: Login auth with DB
	}

    
}

