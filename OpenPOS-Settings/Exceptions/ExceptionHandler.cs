using System.Diagnostics;

namespace OpenPOS_Settings.Exceptions;

public class ExceptionHandler
{
    private static DisplayErrorMessage displayErrorMessage;
    private static WriteToLogHandler writeToLogHandler;
    public static void HandleException(Exception ex, ContentPage originPage, bool writeToLog, bool displayPopup)
    {
        if (writeToLog)
        {
            writeToLogHandler = new WriteToLogHandler(ex);
        }

        if (displayPopup)
        {
            if (originPage != null)
            {
                displayErrorMessage = new DisplayErrorMessage(originPage, ex);
            }
            // TODO: Add popup or screen for when the application hasn't been initialized yet
        }
        Debug.WriteLine(ex.Message);
        Debug.WriteLine(ex.StackTrace);
    }
}