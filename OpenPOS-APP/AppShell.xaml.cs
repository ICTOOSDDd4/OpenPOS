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
		Routing.RegisterRoute(nameof(PaymentPage), typeof(PaymentPage));
		Routing.RegisterRoute(nameof(GoodbyePage), typeof(GoodbyePage));
		Routing.RegisterRoute(nameof(Onboarding), typeof(Onboarding));
    Routing.RegisterRoute(nameof(AdminOverview), typeof(AdminOverview));
    Routing.RegisterRoute(nameof(CrewOverview), typeof(CrewOverview));
    Routing.RegisterRoute(nameof(OrderOverviewPage), typeof(OrderOverviewPage));
    Routing.RegisterRoute(nameof(CreateAccountPage), typeof(CreateAccountPage));
    }
}
