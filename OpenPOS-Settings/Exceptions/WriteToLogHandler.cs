namespace OpenPOS_Settings.Exceptions
{
    public class WriteToLogHandler
    {
        public WriteToLogHandler(Exception e)
        {
            //TODO: Make sure it doesn't create multiple directories
            var path = AppDomain.CurrentDomain.BaseDirectory + "Logs";
            Directory.CreateDirectory(path);

            // TODO: Make sure it doesn't overwrite the file
            File.AppendAllText(path + "/log.txt",
                   DateTime.Now.ToString() + $" {e.Message}. \nException type: {e.GetType()} \nStackTrace: {e.StackTrace}" + Environment.NewLine);
        }
    }
}
