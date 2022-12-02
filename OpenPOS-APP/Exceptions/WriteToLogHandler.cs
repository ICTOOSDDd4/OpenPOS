using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPOS_APP.Exceptions
{
    public class WriteToLogHandler
    {
        public static void WriteToLog(Exception e)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "Logs";
            Directory.CreateDirectory(path);

            File.AppendAllText(path + "/log.txt",
                   DateTime.Now.ToString() + $" {e.Message}. Exception type: {e.GetType}" + Environment.NewLine);
        }
    }
}
