using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace OpenPOS_APP.Exceptions
{
    public static class WriteToLog
    {
        public static void WriteToLog(string message)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "Logs";
            Directory.CreateDirectory(path);
            
            File.AppendAllText(path + "/log.txt",
                   DateTime.Now.ToString() + $" {message}" + Environment.NewLine);
        }

    }

}
