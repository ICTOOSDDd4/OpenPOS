using Microsoft.Extensions.Logging;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services;
using OpenPOS_APP.Services.Models;

namespace OpenPOS_APP;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		Initialize();
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("LeagueSpartan-Black.ttf", "LeagueSpartanBlack");
				fonts.AddFont("LeagueSpartan-Medium.ttf", "LeagueSpartanMedium");
				fonts.AddFont("LeagueSpartan-Regular.ttf", "LeagueSpartanRegular");
				fonts.AddFont("LeagueSpartan-SemiBold.ttf", "LeagueSpartanSemiBold");
				fonts.AddFont("LeagueSpartan-Bold.ttf", "LeagueSpartanBold");
				fonts.AddFont("LeagueSpartan-ExtraBold.ttf", "LeagueSpartanExtraBold");
				fonts.AddFont("LeagueSpartan-ExtraLight.ttf", "LeagueSpartanExtraLight");
				fonts.AddFont("LeagueSpartan-Light.ttf", "LeagueSpartanLight");
				fonts.AddFont("LeagueSpartan-Thin.ttf", "LeagueSpartanThin");
			});

		#if DEBUG
		builder.Logging.AddDebug();
		#endif
        return builder.Build();
	}
	private static void Initialize()
	{
        DatabaseService.Initialize();
        foreach (User user in UserService.GetAll())
        {
            System.Diagnostics.Debug.WriteLine(user.Name);
        }
    }
}
