using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telling.Core.Validation
{
    public class ValidationError
    {
        public string Message { get; set; }

        public bool ForceValidate { get; set; }

        public bool DoLiveValidation { get; set; }
    }
}
