namespace OpenPOS_APP.Resources.Controls;

public partial class AdminHeader : StackLayout
{
	public ContentPage CurrentPage { get; set; }

	public AdminHeader()
	{
		InitializeComponent();
      ChangeColor(1);
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
	
	private void OnTappedCategoryLabel(object sender, EventArgs e)
	{
		ChangeColor(4);
	}
	
	private async void OnClickedLogout(object sender, EventArgs e)
	{
		bool alert = await CurrentPage.DisplayAlert("Loging out", "Are you sure you want to log out?", "Yes", "No");
		if (alert)
		{
			await Shell.Current.GoToAsync(nameof(GoodbyePage));
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