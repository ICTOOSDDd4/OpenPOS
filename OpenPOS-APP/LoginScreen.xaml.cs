
using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Models;
using OpenPOS_APP.Settings;
using System.Reflection;
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
         await Shell.Current.GoToAsync(nameof(TablePickerScreen));

         // If you want to save the user inputs when they press the back button
         // Remove this block of code.
         _password = string.Empty; _username = string.Empty;
         EmailEntry.Text = string.Empty;
         PasswordEntry.Text = string.Empty;

      }
      else { await DisplayAlert("Oops", "You got it wrong", "Try again"); }

   }

   private async void CreateNewAccount_Tapped(object sender, EventArgs e)
   {

      await DisplayAlert("Work in Progress", "This feature is still under development try agian later.", "Alright");
   }
   protected override void OnNavigatedTo(NavigatedToEventArgs args)
   {
      base.OnNavigatedTo(args);
   }

   private bool UserAuth(string username, string password)
   {
      User user = UserService.Authenticate(_username, _password);

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
