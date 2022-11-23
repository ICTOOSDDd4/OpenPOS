namespace OpenPOS_APP;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(CheckoutOverview), typeof(CheckoutOverview));
	}
}
