using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telling.Core.Interfaces;

namespace Telling.Core.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static void Sort<T>(this ObservableCollection<T> collection) where T : ISortableCollection
        {
            var sorted = collection.OrderBy(x => (x as ISortableCollection).Ordinal).ToList();
            for (int i = 0; i < sorted.Count; i++)
                collection.Move(collection.IndexOf(sorted[i]), i);
        }
    }
}
