using OpenPOS_Models;

namespace OpenPOS_Settings.EventArgsClasses
{
   public class OrderEventArgs : EventArgs
   {
      public Order order { get; set; }
   }
}