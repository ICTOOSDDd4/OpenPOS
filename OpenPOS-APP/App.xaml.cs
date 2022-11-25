namespace OpenPOS_APP;

public partial class App : Application
{
    public string ControlTemplate { get; }
    public App()
	 {
      InitializeComponent();

      MainPage = new AppShell();
  }

}
