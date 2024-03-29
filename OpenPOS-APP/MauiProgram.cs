using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using CommunityToolkit.Maui;
using Microsoft.Maui.LifecycleEvents;
using OpenPOS_Controllers.Services;
using OpenPOS_Settings;
using SkiaSharp.Views.Maui.Controls.Hosting;

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
			.UseSkiaSharp()
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
								
		                        p.IsMaximizable = false; // Does not work, Maui Bug
		                        //p.IsAlwaysOnTop = true; // Does work // COMMENT OUT FOR DEV!
								p.IsResizable = false; // Does not work, MAUI Bug
								p.IsMinimizable = false; // Does not work, MAUI Bug
								p.IsModal = false;
							}
							else
							{
								winuiAppWindow.Resize(new SizeInt32(1920, 1080));
								winuiAppWindow.MoveAndResize(new RectInt32(0, 0, 1920, 1080));
							}
							window.ExtendsContentIntoTitleBar = false;
							IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
							WindowId myWndId = Win32Interop.GetWindowIdFromWindow(hWnd);
							var _appWindow = AppWindow.GetFromWindowId(myWndId);
							_appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
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
				UtilityService.StartDatabase(); //TODO: Temp Fix
			}
			
        } else throw new ApplicationException("Can't find appsettings.json file");
    }

}

