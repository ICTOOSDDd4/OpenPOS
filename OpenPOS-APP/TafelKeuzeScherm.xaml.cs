using System.Reflection;
using System.Text.RegularExpressions;

namespace OpenPOS_APP;

public partial class TafelKeuzeScherm : ContentPage
{
   private int _tableNumber;
   private ResourceDictionary _appColors = new();

   public TafelKeuzeScherm()
	{
		InitializeComponent();
      _appColors.SetAndLoadSource(new Uri("Resources/Styles/Colors.xaml", UriKind.RelativeOrAbsolute), "Resources/Styles/Colors.xaml", this.GetType().GetTypeInfo().Assembly, null);

   }
   protected override void OnNavigatedTo(NavigatedToEventArgs args)
   {
      base.OnNavigatedTo(args);
   }

   private async void OnSubmitButtonClicked(object sender, EventArgs e)
   {
      await DisplayAlert("Oops", "Hahaha, That didn't work.", "Try again");
   }

   private void OnTableNumberEntryChanged(object sender, TextChangedEventArgs e)
   {
      int value;
      if (int.TryParse(e.NewTextValue, out value))
      {
         _tableNumber = value;
         ErrorDisplayLabel.IsVisible = false;
         ActivateButton(true);

      }
      else if (e.NewTextValue == string.Empty)
      {
         ErrorDisplayLabel.Text = "You need to input a number to continue";
         ErrorDisplayLabel.IsVisible = true;
         ActivateButton(false);
      } 
      else
      {
         
         ErrorDisplayLabel.Text = "You can only input numbers.";
         ErrorDisplayLabel.IsVisible = true;
         string placeholder = e.NewTextValue;
         //TODO: Fix bug that doesn't allow you to have numeric string with more then 10 chars.
         //Temp fix: Made maxLength 10 for now.
         placeholder = Regex.Replace(placeholder, "[^0-9]+", ""); 
         TableNumberEntry.Text = placeholder; 
         ActivateButton(false);
         
      }

   }

   private void ActivateButton(bool active)
   {
      if (active)
      {
         SubmitButton.IsEnabled = true;
         if (_appColors.TryGetValue("OpenPos-Green", out var color)) {
            SubmitButton.BackgroundColor = (Color)color;
        }
      } 
      else
      {
         SubmitButton.IsEnabled = false;
         if (_appColors.TryGetValue("Gray100", out var color))
         {
            SubmitButton.BackgroundColor = (Color)color;
         }
      }
   }
}