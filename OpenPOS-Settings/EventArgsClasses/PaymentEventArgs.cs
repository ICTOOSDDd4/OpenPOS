using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenPOS_Models;

namespace OpenPOS_Settings.EventArgsClasses
{
    public class PaymentEventArgs : EventArgs
    {
        public Tikkie Tikkie { get; set; }
    }
}
