using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPOS_APP.Settings
{
    public class ApiSettings
    {
        private string _base_url;
        public string base_url
        {
            get
            {
                return _base_url;
            }
            set
            {
                ArgumentNullException.ThrowIfNullOrEmpty(nameof(value));
                _base_url = value;
            }
        }
    }
}
