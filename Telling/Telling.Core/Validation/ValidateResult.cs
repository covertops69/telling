using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telling.Core.Helpers;
using Telling.Core.Validators;

namespace Telling.Core.Validation
{
    public class ValidateResult
    {
        public bool IsValid { get; set; }
        public ObservableDictionary<string, ValidationError> Errors { get; set; }
    }
}
