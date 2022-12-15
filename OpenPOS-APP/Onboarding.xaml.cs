using System.Reflection;

namespace OpenPOS_APP;

public partial class Onboarding : ContentPage
{
	private readonly ResourceDictionary _appColors = new();
	public Onboarding()
	{
        InitializeComponent();
        _appColors.SetAndLoadSource(new Uri("Resources/Styles/Colors.xaml", UriKind.RelativeOrAbsolute), "Resources/Styles/Colors.xaml", this.GetType().GetTypeInfo().Assembly, null );
        OnIconLoaded();
    }

    private async void OnIconLoaded()
    {
        await Openposicon.RelRotateTo(360, 4000);
    }

	private async void OnLoginButtonClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(LoginScreen));
	}
}

