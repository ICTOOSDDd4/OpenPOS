using OpenPOS_APP.Models;

namespace OpenPOS_Settings.EventArgsClasses
{
   public class OrderEventArgs : EventArgs
   {
      public Order order { get; set; }
   }
}