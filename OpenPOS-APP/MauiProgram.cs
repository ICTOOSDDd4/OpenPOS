using Microsoft.Extensions.Logging;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services;

namespace OpenPOS_APP;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		Initilize();
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		#if DEBUG
		builder.Logging.AddDebug();
		#endif
        return builder.Build();
	}
	private static void Initilize()
	{
        DatabaseService.Initialize();
        foreach (User user in UserService.GetAll())
        {
            System.Diagnostics.Debug.WriteLine(user.Name);
        }
    }
}
