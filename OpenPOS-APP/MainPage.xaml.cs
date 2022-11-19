using Microsoft.Extensions.Configuration;
using OpenPOS_APP.Services;
using OpenPOS_APP.Settings;

namespace OpenPOS_APP;

public partial class MainPage : ContentPage
{
	private int _count;

	public MainPage(IConfiguration config)

	{
		InitializeComponent();

		// Starting up DB connection
		var settings = config.GetRequiredSection("DATABASE_CONNECTION").Get<DatabaseSettings>();
		if (settings != null) DatabaseService.Initialize(settings.DatabaseConnectionString);
	}

	private async void OnCounterClicked(object sender, EventArgs e)
	{
		_count++;

		if (_count == 1)
			CounterBtn.Text = $"Clicked {_count} time";
		else
			CounterBtn.Text = $"Clicked {_count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}

    
}

