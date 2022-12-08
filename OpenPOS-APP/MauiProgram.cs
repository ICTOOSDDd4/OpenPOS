using Microsoft.Extensions.Logging;
using OpenPOS_APP.Services;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using OpenPOS_APP.Settings;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Models;
using CommunityToolkit.Maui;
using System.Diagnostics;
using Microsoft.Maui.LifecycleEvents;

// Specific WinUI elements.
#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif


namespace OpenPOS_APP;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
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
            Initialize();
			

      // Windows specific window size settings
#if WINDOWS
      builder.ConfigureLifecycleEvents(events =>
				{
					events.AddWindows(windows => windows.OnClosed((window, args) => args.Handled = true));
					events.AddWindows(wndLifeCycleBuilder =>
					{
						wndLifeCycleBuilder.OnWindowCreated(window =>
						{
							IntPtr nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
							WindowId win32WindowsId = Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
							AppWindow winuiAppWindow = AppWindow.GetFromWindowId(win32WindowsId);
							if (winuiAppWindow.Presenter is OverlappedPresenter p)
							{
								p.Maximize(); // Does work
                        p.IsMaximizable = false; // Does not work
                        //p.IsAlwaysOnTop = true; // Does work // COMMENT OUT FOR DEV!
								p.IsResizable = false; // Does not work
								p.IsMinimizable = false; // Does not work
								p.IsModal = false;
							}
							else
							{
								winuiAppWindow.Resize(new SizeInt32(1920, 1080));
								winuiAppWindow.MoveAndResize(new RectInt32(0, 0, 1920, 1080));
								//winuiAppWindow.MoveAndResize(new RectInt32(1920 / 2 - width / 2, 1080 / 2 - height / 2, width, height));
							}
						});
					});
				});
#endif

#if DEBUG
      builder.Logging.AddDebug();
#endif
      return builder.Build();
	}
	private static void Initialize()
	{
		var a = Assembly.GetExecutingAssembly();
		using var stream = a.GetManifestResourceStream("OpenPOS_APP.appsettings.json");
		if (stream != null)
		{
			var config = new ConfigurationBuilder()
				.AddJsonStream(stream)
				.Build();

			ApplicationSettings.DbSett = config.GetRequiredSection("DATABASE_CONNECTION").Get<DatabaseSettings>();
			ApplicationSettings.TikkieSet = config.GetRequiredSection("TIKKIE_API").Get<TikkieSettings>();
			ApplicationSettings.QRCodeGeneratorSet = config.GetRequiredSection("QR_CODE_GENERATOR").Get<QRCodeGeneratorSettings>();
         ApplicationSettings.ApiSet = config.GetRequiredSection("OPENPOS_API").Get<ApiSettings>();
			
			if (ApplicationSettings.DbSett != null) 
			{
				DatabaseService.Initialize();
			}
            Bill bill = BillService.FindByID(12);
            bill.Paid = false;
            var result = BillService.Update(bill);
			Debug.WriteLine(result);
        } else throw new ApplicationException("Can't find appsettings.json file");
		
		ApplicationSettings.UIElements = new UIElements();
   
	}
}
