namespace OpenPOS_APP;

public partial class TafelKeuzeScherm : ContentPage
{
	public TafelKeuzeScherm()
	{
		InitializeComponent();
	}

   private async void OnLoginButtonClicked(object sender, EventArgs e)
   {
      await DisplayAlert("Oops", "You got it wrong", "Try again");
   }

   protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}