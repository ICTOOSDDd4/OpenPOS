using System.Reflection;
using System.Text.RegularExpressions;
using OpenPOS_Controllers;
using OpenPOS_Models;
using OpenPOS_Settings;

namespace OpenPOS_APP;

public partial class TablePickerScreen : ContentPage
{
    private int _tableNumber;
    private readonly ResourceDictionary _appColors = new();
    private readonly TableController _tableController = new();
    private readonly BillController _billController = new();

    public TablePickerScreen()
    {
        InitializeComponent();
        _appColors.SetAndLoadSource(new Uri("Resources/Styles/Colors.xaml", UriKind.RelativeOrAbsolute), "Resources/Styles/Colors.xaml", this.GetType().GetTypeInfo().Assembly, null);
    }
    
    private async void OnSubmitButtonClicked(object sender, EventArgs e)
    {
        SubmitButton.IsVisible = false;
        LoadingIndicator.IsRunning = true;
        LoadingIndicator.IsVisible = true;

        string entryString = TableNumberEntry.Text;
        if (int.TryParse(entryString.Trim(), out int value))
        {
            if (!_tableController.CheckForOpenBill(value))
            {
                _tableNumber = value;
                ApplicationSettings.TableNumber = _tableNumber;
                Table table = _tableController.GetByTableNumber(_tableNumber);
                if (table == null)
                {
                    ErrorDisplayLabel.Text = "This isn't a valid table.";
                    ErrorDisplayLabel.IsVisible = true;
                    ActivateButton(false);
                } 
                else
                {
                    ApplicationSettings.CurrentBill = _billController.CreateBill(ApplicationSettings.LoggedinUser.Id);
                    table.Bill_id = ApplicationSettings.CurrentBill.Id;
                    if (_tableController.AttachBillToTable(_tableNumber, ApplicationSettings.CurrentBill.Id))
                    {
                        await Shell.Current.GoToAsync(nameof(MenuPage));
                    }
                }
            }
            else
            {
                SubmitButton.IsVisible = true;
                LoadingIndicator.IsRunning = false;
                LoadingIndicator.IsVisible = false;
                await DisplayAlert("Error", "This table already has been taken. \n Please call for one of our staff members.", "OK");
            }
        }
        else
        {
            SubmitButton.IsVisible = true;
            LoadingIndicator.IsRunning = false;
            LoadingIndicator.IsVisible = false;
        }
    }

    private void OnTableNumberEntryChanged(object sender, TextChangedEventArgs e)
    {
        // Keeping the values explicit for readability
        int value;
        string oldValue = e.OldTextValue;
        
        if (int.TryParse(e.NewTextValue, out value))
        {
            _tableNumber = value;
            if (oldValue != null)
            {
                if (oldValue.Any(char.IsLetter))
                {
                    ErrorDisplayLabel.IsVisible = true;
                }
                else
                {
                    ErrorDisplayLabel.IsVisible = false;
                }
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

    private void ActivateButton(bool active) // This is a method that I made to activate or deactivate the submit button.
    {
        if (active)
        {

            if (_appColors.TryGetValue("OpenPos-Green", out var color))
            {
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