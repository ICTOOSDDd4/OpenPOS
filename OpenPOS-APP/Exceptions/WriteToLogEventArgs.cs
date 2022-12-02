using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPOS_APP.Exceptions
{
    public class WriteToLogEventArgs : EventArgs
    {
        public string message;

        public WriteToLogEventArgs()
        {

        }
        public WriteToLogEventArgs (string message)
        {
            this.message = message;
        }
    }
}
