namespace OpenPOS_Settings.Exceptions
{
    public class WriteToLogHandler
    {
        public WriteToLogHandler(Exception e)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "Logs";
            Directory.CreateDirectory(path);

            File.AppendAllText(path + "/log.txt",
                   DateTime.Now.ToString() + $" {e.Message}. \nException type: {e.GetType()} \nStackTrace: {e.StackTrace}" + Environment.NewLine);
        }
    }
}
