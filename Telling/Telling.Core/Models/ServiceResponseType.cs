using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telling.Core.Models
{
    public enum ServiceReponseType
    {
        Successful = 0,
        Error = 1,
        Timeout = 2,

        NoNetwork = 3, // TODO: currently just throws an error
    }
}
