using Windows.UI.ViewManagement;

namespace OpenPOS_APP;

public partial class App : Application
{
    public string ControlTemplate { get; }
    public App()
	 {
      InitializeComponent();
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
