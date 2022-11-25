using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Models;
using OpenPOS_APP.Settings;
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
      string entryString = TableNumberEntry.Text;
      if (int.TryParse(entryString.ToString().Trim(), out int value))
      {
         _tableNumber = value;
         Table table = TableService.FindByTableNumber(_tableNumber);
         if (table != null)
         {
            ErrorDisplayLabel.Text = "This isn't a valid table.";
            ErrorDisplayLabel.IsVisible = true;
            ActivateButton(false);
         } else
         {
            Bill bill = new Bill(value, ApplicationSettings.LoggedinUser.Id, false, DateTime.Now, DateTime.Now);
            ApplicationSettings.CurrentBill = BillService.Create(bill);
            table.Bill_id = bill.Id;
            if (TableService.Update(table))
            {
               await Shell.Current.GoToAsync(nameof(MenuPage));
            }
         }
      }
      
   }

   private void OnTableNumberEntryChanged(object sender, TextChangedEventArgs e)
   {
      int value;
      string oldValue = e.OldTextValue;
      string newValue = e.NewTextValue;

      if (int.TryParse(e.NewTextValue, out value))
      {
         _tableNumber = value;
         if (oldValue != null)
         {
            if (oldValue.Any(e => char.IsLetter(e)))
            {
               ErrorDisplayLabel.IsVisible = true;
            }
            else { ErrorDisplayLabel.IsVisible = false; }
         }
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
         
         if (_appColors.TryGetValue("OpenPos-Green", out var color)) {
            SubmitButton.BackgroundColor = (Color)color;
            SubmitButton.IsEnabled = true;
         }
      } 
      else
      {
         
         if (_appColors.TryGetValue("Gray100", out var color))
         {
            SubmitButton.BackgroundColor = (Color)color;
            SubmitButton.IsEnabled = false;
         }
      }
   }
}