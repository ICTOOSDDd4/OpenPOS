using OpenPOS_Settings;

namespace OpenPOS_APP.Resources.Controls;

public partial class AdminHeader : StackLayout
{
	public ContentPage CurrentPage { get; set; }

	public AdminHeader()
	{
		InitializeComponent();
		ChangeColor(1); // Marks Dashboard as selected
   }

   private async void OnTappedDashboardLabel(object sender, EventArgs e)
	{
		ChangeColor(1);
		if (CurrentPage.GetType() != typeof(AdminDashboardPage))
		{
			await Shell.Current.GoToAsync(nameof(AdminDashboardPage));
		}
		
	}
	
	private void OnTappedUserLabel(object sender, EventArgs e)
	{
		ChangeColor(2);
	}
	
	private void OnTappedProductLabel(object sender, EventArgs e)
	{
		ChangeColor(3);
	}
	
	private async void OnTappedCategoryLabel(object sender, EventArgs e)
	{
		ChangeColor(4);
		await Shell.Current.GoToAsync(nameof(AdminCategoryPage));
	}
	
	private async void OnClickedLogout(object sender, EventArgs e)
	{
		bool alert = await CurrentPage.DisplayAlert("Logging out", "Are you sure you want to log out?", "Yes", "No");
		if (alert)
		{
			ApplicationSettings.LoggedinUser = null;
			await Shell.Current.GoToAsync(nameof(Onboarding));
		}
	}
	
	private void ChangeColor(int item)
	{
		if (item == 1)
		{
			DashboardLabel.TextColor = Color.FromRgb(255, 0, 0);
			UserLabel.TextColor = Color.FromRgb(0, 0, 0);
			ProductLabel.TextColor = Color.FromRgb(0, 0, 0);
			CategoryLabel.TextColor = Color.FromRgb(0, 0, 0);
		} 
		else if (item == 2)
		{
			UserLabel.TextColor = Color.FromRgb(255, 0, 0);
			DashboardLabel.TextColor = Color.FromRgb(0, 0, 0);
			ProductLabel.TextColor = Color.FromRgb(0, 0, 0);
			CategoryLabel.TextColor = Color.FromRgb(0, 0, 0);
		}
		else if (item == 3)
		{
			ProductLabel.TextColor = Color.FromRgb(255, 0, 0);
			DashboardLabel.TextColor = Color.FromRgb(0, 0, 0);
			UserLabel.TextColor = Color.FromRgb(0, 0, 0);
			CategoryLabel.TextColor = Color.FromRgb(0, 0, 0);
		}
		else if (item == 4)
		{
			CategoryLabel.TextColor = Color.FromRgb(255, 0, 0);
			DashboardLabel.TextColor = Color.FromRgb(0, 0, 0);
			UserLabel.TextColor = Color.FromRgb(0, 0, 0);
			ProductLabel.TextColor = Color.FromRgb(0, 0, 0);
		}
	}
}