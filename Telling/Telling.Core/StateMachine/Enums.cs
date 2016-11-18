using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telling.Core.StateMachine
{
    public enum Trigger
    {
        NavigateToList,
        ShowAdd,
        PresentModal
    }

    public enum State
    {
        Launch,
        ListView,
        AddView,
        Modal
    }
}
