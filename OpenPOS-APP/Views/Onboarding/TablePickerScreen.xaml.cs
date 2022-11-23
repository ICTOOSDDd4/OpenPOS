using Microsoft.Maui;
using System.Reflection;
using System.Text.RegularExpressions;

namespace OpenPOS_APP;

public partial class TablePickerScreen : ContentPage
{
   private int _tableNumber;
   private ResourceDictionary _appColors = new();

   public TablePickerScreen()
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
      string oldValue = e.OldTextValue;
      string newValue = e.NewTextValue;

      if (e.OldTextValue.Any(c => char.IsLetter(c)) || e.NewTextValue.Any(c => char.IsLetter(c)))
      {
         ErrorDisplayLabel.Text = "You can only input numbers.";
         ErrorDisplayLabel.IsVisible = true;
         string placeholder = Regex.Replace(e.NewTextValue, "[^0-9]+", "");
         TableNumberEntry.Text = placeholder;
         ActivateButton(false);
      }

      if (int.TryParse(e.NewTextValue, out value))
      {
         _tableNumber = value;
         ErrorDisplayLabel.IsVisible = false;
         ActivateButton(true);

      }
      else if (e.NewTextValue.Trim() == string.Empty)
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