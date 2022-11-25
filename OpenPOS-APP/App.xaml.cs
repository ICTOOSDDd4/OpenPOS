namespace OpenPOS_APP;

public partial class App : Application
{
    public string ControlTemplate { get; }
    public App()
	 {
      InitializeComponent();
      Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
      {
#if WINDOWS
            var mauiWindow = handler.VirtualView;
            var nativeWindow = handler.PlatformView;
            nativeWindow.Activate();
            IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
            WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
            AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
            appWindow.Resize(new SizeInt32(WindowWidth, WindowHeight));
#endif
      });
      MainPage = new AppShell();
  }

   private void OnSearch(object sender, TextChangedEventArgs e)
   {

   }

    private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
    {

   }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {

   }
}
