namespace OpenPOS_Settings
{
   public class DatabaseSettings
   {
      private string _connection_string;
      public string connection_string
      {
         get
         {
            return _connection_string;
         }
         set
         {
            ArgumentNullException.ThrowIfNullOrEmpty(nameof(value));
            _connection_string = value;
         }
      }

   }
}
