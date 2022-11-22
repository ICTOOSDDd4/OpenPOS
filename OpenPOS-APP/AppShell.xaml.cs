namespace OpenPOS_APP;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(TafelKeuzeScherm), typeof(TafelKeuzeScherm));
	}
}
