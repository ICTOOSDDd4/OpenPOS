using System.Reflection;
using System.Text.RegularExpressions;

namespace OpenPOS_APP;

public partial class CreateAccountPage : ContentPage
{
	private string _password;
	private bool _isValid = false;
	
	private ResourceDictionary _appColors = new();

	
	public CreateAccountPage()
	{
		InitializeComponent();
		_appColors.SetAndLoadSource(new Uri("Resources/Styles/Colors.xaml", UriKind.RelativeOrAbsolute), "Resources/Styles/Colors.xaml", this.GetType().GetTypeInfo().Assembly, null);
	}

	private void ActivateButton(bool active)
	{
		if (active)
		{
         
			if (_appColors.TryGetValue("OpenPos-Green", out var color)) {
				CreateAccountButton.BackgroundColor = (Color)color;
				CreateAccountButton.IsEnabled = true;
			}
		} 
		else
		{
         
			if (_appColors.TryGetValue("Gray100", out var color))
			{
				CreateAccountButton.BackgroundColor = (Color)color;
				CreateAccountButton.IsEnabled = false;
			}
		}
	}

	private bool CheckAllFieldsValid()
	{
		ActivateButton(false);
		//First name check
		if (FirstName.Text is not { Length: > 1 })
		{
			SetErrorLabel("First name must be at least 2 characters long");
			return false;
		}
		
		//Last name check
		if (LastName.Text is not { Length: > 1 })
		{
			SetErrorLabel("Last name must be at least 2 characters long");
			return false;
		}

		//Email check
		if (EmailEntry.Text == null || !Regex.IsMatch(EmailEntry.Text, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
		{
			SetErrorLabel("Email is not valid");
			return false;
		}
		
		//Password check
		if (PasswordEntry.Text is not { Length: >= 8 })
		{
			SetErrorLabel("Password must be at least 8 characters long");
			return false;
		}
		
		//Password check verify
		if (PasswordEntry.Text != PasswordEntryVerify.Text)
		{
			SetErrorLabel("Passwords do not match");
			return false;
		}

		ActivateButton(true);
		ClearErrorLabel();
		return true;
	}

	private void SetErrorLabel(string errorText)
	{
		ErrorDisplayLabel.Text = errorText;
		ErrorDisplayLabel.IsVisible = true;
		_isValid = false;
	}
	private void ClearErrorLabel()
	{
		ErrorDisplayLabel.Text = "";
		ErrorDisplayLabel.IsVisible = false;
		_isValid = true;
	}
	
	

	private void OnTextChanged(object sender, TextChangedEventArgs e)
	{
		CheckAllFieldsValid();
	}

	private void OnCreateAccountButtonClicked(object sender, EventArgs e)
	{
		if (CheckAllFieldsValid())
		{
			Console.WriteLine("Account created");
			//TODO: Check for email in database
			//TODO: Create account if not found in DB
		}
		else
		{
			Console.WriteLine("Not allowed!");
		}
	}
}
