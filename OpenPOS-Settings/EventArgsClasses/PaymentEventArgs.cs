using OpenPOS_APP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPOS_Settings.EventArgsClasses
{
    public class PaymentEventArgs : EventArgs
    {
        public Tikkie Tikkie { get; set; }
    }
}
