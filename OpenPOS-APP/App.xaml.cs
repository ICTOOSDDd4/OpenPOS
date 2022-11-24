namespace OpenPOS_APP;

public partial class App : Application
{
    public string ControlTemplate { get; }
    public App()
	{
		InitializeComponent();
        ControlTemplate = "{StaticResource MyControlTemplate}";
        MainPage = new AppShell();
    }

    private void OnSearch(object sender, TextChangedEventArgs e)
    {
    }
}
