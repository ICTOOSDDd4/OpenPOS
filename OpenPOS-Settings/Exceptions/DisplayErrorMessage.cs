namespace OpenPOS_Settings.Exceptions;

public class DisplayErrorMessage
{
    public DisplayErrorMessage(ContentPage page, Exception e)
    {
        page.DisplayAlert("Error: ", e.Message, "OK");
    }
}