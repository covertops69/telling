using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telling.Core.Helpers;

namespace Telling.Core.Extensions
{
    public static class ListExtensions
    {
        public static ObservableDictionary<TKey, TElement> ToObservableDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            if (source == null || keySelector == null || elementSelector == null)
                return new ObservableDictionary<TKey, TElement>();

            var dictionary = new ObservableDictionary<TKey, TElement>();

            foreach (TSource element in source)
                dictionary.Add(keySelector(element), elementSelector(element));

            return dictionary;
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> list) where T : class
        {
            return new ObservableCollection<T>(list ?? new List<T>());
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list) where T : class
        {
            return list == null || list.ToList().Count == 0;
        }
    }
}
