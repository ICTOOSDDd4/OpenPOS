using System.Reflection;
using OpenPOS_Controllers;
using OpenPOS_Settings;
using OpenPOS_Settings.Enums;
using OpenPOS_Models;
namespace OpenPOS_APP;

public partial class LoginScreen : ContentPage
{
    private string _username;
    private string _password;
    private readonly ResourceDictionary _appColors = new();
    private readonly AuthenticationController _authenticationController;

    public LoginScreen()
    {
        _authenticationController = new AuthenticationController();
        InitializeComponent();
        _appColors.SetAndLoadSource(new Uri("Resources/Styles/Colors.xaml", UriKind.RelativeOrAbsolute),
            "Resources/Styles/Colors.xaml", this.GetType().GetTypeInfo().Assembly, null);
    }
    
    private void OnTextChangedUsername(object sender, TextChangedEventArgs e)
    {
        _username = e.NewTextValue;
        if (string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_username))
        {
            ActivateButton(false);
        }
        else
        {
            ActivateButton(true);
        }
    }

    private void OnTextChangedPassword(object sender, TextChangedEventArgs e)
    {
        _password = e.NewTextValue;
        if (string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_username))
        {
            ActivateButton(false);
        }
        else
        {
            ActivateButton(true);
        }
    }

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        MainLoginButton.IsVisible = false;
        MainLoginButton.IsEnabled = false;
        LoadingIndicator.IsRunning = true;
        LoadingIndicator.IsEnabled = true;
        if (UserAuth(_username, _password))
        {
            var role = Enum.Parse<RolesEnum>(_authenticationController.GetUserRole(_username, _password).Title);
            switch (role)
            {
                case (RolesEnum.Owner or RolesEnum.Admin):
                    await Shell.Current.GoToAsync(nameof(AdminDashboardPage));
                    break;
                case (RolesEnum.Crew):
                    await Shell.Current.GoToAsync(nameof(CrewOverview));
                    break;
                case (RolesEnum.Cook or RolesEnum.Bar):
                    await Shell.Current.GoToAsync(nameof(OrderOverviewPage));
                    break;
                case (RolesEnum.Guest):
                    await Shell.Current.GoToAsync(nameof(TablePickerScreen));
                    break;
                default:
                    await DisplayAlert("Error 403", "Forbidden: You are not authorized", "Understood");
                    break;
            }

            // If you want to save the user inputs when they press the back button
            // Remove this block of code.
            _password = string.Empty;
            _username = string.Empty;
            EmailEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;
        }
        else
        {
            LoadingIndicator.IsVisible = false;
            LoadingIndicator.IsRunning = false;
            MainLoginButton.IsVisible = true;
            await DisplayAlert("Invalid credentials", "This username and/or password are not correct.", "Try again");
        }
    }

    private async void CreateNewAccount_Tapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CreateAccountPage));
    }

    private bool UserAuth(string username, string password)
    {
        User user = _authenticationController.Authenticate(username, password);

        if (user != null)
        {
            ApplicationSettings.LoggedinUser = user;
            return true;
        }
        else
        {
            return false;
        }
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        // Makes the button visible & enables it when navigated back to the page with the back button
        base.OnNavigatedTo(args);
        MainLoginButton.IsVisible = true; 
        MainLoginButton.IsEnabled = true;
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

