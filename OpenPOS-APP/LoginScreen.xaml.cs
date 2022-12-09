
using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Models;
using OpenPOS_APP.Settings;
using System.Reflection;
using OpenPOS_APP.Enums;
using OpenPOS_APP.Services;


namespace OpenPOS_APP.Views.Onboarding;

public partial class LoginScreen : ContentPage
{
    
   private string _username;
   private string _password;
   private ResourceDictionary _appColors = new();
   public LoginScreen()
   {
      InitializeComponent();
      _appColors.SetAndLoadSource(new Uri("Resources/Styles/Colors.xaml", UriKind.RelativeOrAbsolute), "Resources/Styles/Colors.xaml", this.GetType().GetTypeInfo().Assembly, null);
   }
   private void OnTextFilledUsername(object sender, TextChangedEventArgs e)
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

   private void OnTextFilledPassword(object sender, TextChangedEventArgs e)
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
      if (UserAuth(_username, _password))
      {
            var role = Enum.Parse<RolesEnum>(RoleService.FindUserRole(UserService.Authenticate(_username, _password).Id).Title);
            switch (role)            {
                case (RolesEnum.Owner or RolesEnum.Admin):
                    await Shell.Current.GoToAsync(nameof(AdminOverview));
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
            _password = string.Empty; _username = string.Empty;
          EmailEntry.Text = string.Empty;
          PasswordEntry.Text = string.Empty;

      }
      else { await DisplayAlert("Invalid credentials", "This username and/or password are not correct.", "Try again"); }

   }

   private async void CreateNewAccount_Tapped(object sender, EventArgs e)
   {

      await Shell.Current.GoToAsync(nameof(CreateAccountPage));
      // await DisplayAlert("Work in Progress", "This feature is still under development try agian later.", "Alright");
   }
   protected override void OnNavigatedTo(NavigatedToEventArgs args)
   {
      base.OnNavigatedTo(args);
   }

   private bool UserAuth(string username, string password)
   {
     
      User user = UserService.Authenticate(username, password);
      
      if (user != null)
      {
         ApplicationSettings.LoggedinUser = user;
         return true;
      }
      else { return false; }
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

