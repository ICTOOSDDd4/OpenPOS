using System.Reflection;
using System.Text.RegularExpressions;
using OpenPOS_Controllers;
using OpenPOS_Controllers.Services;
using OpenPOS_Models;
using OpenPOS_Settings;
using OpenPOS_Settings.Enums;
using TextChangedEventArgs = Microsoft.Maui.Controls.TextChangedEventArgs;

namespace OpenPOS_APP;

public partial class CreateAccountPage : ContentPage
{
	
	private readonly ResourceDictionary _appColors = new();
	private readonly UserController _userController = new();
	private readonly RoleController _roleController = new();


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
	}
	private void ClearErrorLabel()
	{
		ErrorDisplayLabel.Text = "";
		ErrorDisplayLabel.IsVisible = false;
	}
	
	

	private void OnTextChanged(object sender, TextChangedEventArgs e)
	{
		CheckAllFieldsValid();
	}

	private void OnCreateAccountButtonClicked(object sender, EventArgs e)
	{
		if (CheckAllFieldsValid())
		{
			if (_userController.FindByEmail(EmailEntry.Text) == null)
			{
				User currentUser = _userController.CreateNew(FirstName.Text, LastName.Text, EmailEntry.Text, PasswordEntry.Text);
				
				
				Role guestRole = _roleController.FindOnTitle(RolesEnum.Guest);
				_userController.AddRoleToUser(currentUser.Id, guestRole.Id);
				
				ApplicationSettings.LoggedinUser = currentUser;
				RedirectToMainPage();
			}
			else
			{
				SetErrorLabel("Email already in use");
			}
		}
	}

	private async void RedirectToMainPage()
	{
		await Shell.Current.GoToAsync(nameof(TablePickerScreen));
	}
}
