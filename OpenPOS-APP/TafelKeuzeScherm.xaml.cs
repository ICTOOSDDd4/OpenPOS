namespace OpenPOS_APP;

public partial class TafelKeuzeScherm : ContentPage
{
   private int _tableNumber;
	public TafelKeuzeScherm()
	{
		InitializeComponent();
	}

   private async void OnLoginButtonClicked(object sender, EventArgs e)
   {
      await DisplayAlert("Oops", "Hahaha, That didn't work.", "Try again");
   }

   protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }

   private void OnTableNumberEntryChanged(object sender, TextChangedEventArgs e)
   {
      int value;
      if (int.TryParse(e.NewTextValue, out value))
      {
         _tableNumber = value;
      }  else throw new ArgumentOutOfRangeException("That ain't a number");

   }

}