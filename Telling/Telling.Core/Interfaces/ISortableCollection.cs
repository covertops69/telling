using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telling.Core.Interfaces
{
    public interface ISortableCollection : IComparable
    {
        int Ordinal { get; }
    }
}
