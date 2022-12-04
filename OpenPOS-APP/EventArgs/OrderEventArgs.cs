using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenPOS_APP.Models;

namespace OpenPOS_APP.NewFolder
{
   public class OrderEventArgs : EventArgs
   {
      public Order order { get; set; }
   }
}