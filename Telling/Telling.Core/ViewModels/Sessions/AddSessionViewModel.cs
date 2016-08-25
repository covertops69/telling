using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telling.Core.ViewModels.Sessions
{
    class AddSessionViewModel : BaseValidationViewModel
    {
        private DateTime _sessionDate;
        public DateTime SessionDate
        {
            get
            {
                return _sessionDate;
            }
            set
            {
                if (SetProperty(ref _sessionDate, value))
                {
                    SetError(() => SessionDate);
                }
            }
        }
    }
}
