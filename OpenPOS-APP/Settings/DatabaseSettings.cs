using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace OpenPOS_APP.Settings
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
