using System.Reflection;
using OpenPOS_Controllers.Services;
using OpenPOS_Settings.Exceptions;

namespace OpenPOS_APP;

public partial class MainPage : ContentPage
{
	private readonly ResourceDictionary _appColors = new();
	public MainPage()
	{
      InitializeComponent();
      _appColors.SetAndLoadSource(new Uri("Resources/Styles/Colors.xaml", UriKind.RelativeOrAbsolute), "Resources/Styles/Colors.xaml", this.GetType().GetTypeInfo().Assembly, null );
      OnIconLoaded();
      try
      {
	      UtilityService.StartDatabase();
      }
      catch (Exception ex)
      {
	      ExceptionHandler.HandleException(ex, this, true, true);
      }

	}

	private async void OnIconLoaded()
	{
		await OpenPosIcon.RelRotateTo(360, 4000);
	}
	
    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(LoginScreen)); 
    }
}

