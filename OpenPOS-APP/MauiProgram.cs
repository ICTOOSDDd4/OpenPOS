using Microsoft.Extensions.Logging;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services;
using OpenPOS_APP.Services.Models;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using OpenPOS_APP.Settings;

namespace OpenPOS_APP;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
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
		
		// Searching and importing the appsettings.json file
      var a = Assembly.GetExecutingAssembly();
      using var stream = a.GetManifestResourceStream("OpenPOS_APP.appsettings.json");

      if (stream != null)
      {
	      // Adding config file into the MAUI configuration
	      var config = new ConfigurationBuilder()
		      .AddJsonStream(stream)
		      .Build();
	      builder.Configuration.AddConfiguration(config);

	      // Use Add Transient to add the configuration to the right page.
	      builder.Services.AddTransient<MainPage>();
      }
      
#if DEBUG
      builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
	// private static void Initialize()
	// {
 //        DatabaseService.Initialize();
 //        foreach (User user in UserService.GetAll())
 //        {
 //            System.Diagnostics.Debug.WriteLine(user.Name);
 //        }
 //    }
}
