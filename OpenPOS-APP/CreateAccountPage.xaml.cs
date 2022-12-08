using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using OpenPOS_APP.Enums;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services;
using OpenPOS_APP.Services.Models;
using OpenPOS_APP.Settings;

namespace OpenPOS_APP;

public partial class CreateAccountPage : ContentPage
{
	
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
			if (UserService.FindByEmail(EmailEntry.Text) == null)
			{
				//TODO: Encrypt passwords (OPENPOS-11)
				string encryptedPassword = HashingService.HashPassword(PasswordEntry.Text);


				User currentUser = UserService.Create(new User
				{
					Name = FirstName.Text,
					Last_name = LastName.Text,
					Email = EmailEntry.Text,
					Password = encryptedPassword
				});
				
				Role guestRole = RoleService.FindOnRoleTitle(RolesEnum.Guest);
				UserRoleService.Create(new UserRole
				{
					User_id = currentUser.Id,
					Role_id = guestRole.Id
				});
				
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
