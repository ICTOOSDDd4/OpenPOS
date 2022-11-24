using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPOS_APP.Exceptions.Interfaces
{
    interface IException
    {
        List<string> Errors { get; }


    }
}
