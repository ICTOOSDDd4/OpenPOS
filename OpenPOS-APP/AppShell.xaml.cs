using OpenPOS_APP.Views.Onboarding;

namespace OpenPOS_APP;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(CheckoutOverview), typeof(CheckoutOverview));
		Routing.RegisterRoute(nameof(TablePickerScreen), typeof(TablePickerScreen));
		Routing.RegisterRoute(nameof(LoginScreen), typeof(LoginScreen));
		Routing.RegisterRoute(nameof(MenuPage), typeof(MenuPage));
        Routing.RegisterRoute(nameof(AdminOverview), typeof(AdminOverview));
        Routing.RegisterRoute(nameof(KitchenOverview), typeof(KitchenOverview));
        Routing.RegisterRoute(nameof(CrewOverview), typeof(CrewOverview));
        Routing.RegisterRoute(nameof(BarOverview), typeof(BarOverview));
        Routing.RegisterRoute(nameof(OwnerOverview), typeof(OwnerOverview));
        Routing.RegisterRoute(nameof(OrderOverviewPage), typeof(OrderOverviewPage));
    }
}
