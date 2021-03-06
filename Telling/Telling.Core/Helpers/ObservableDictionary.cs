﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Telling.Core.Helpers
{
    public class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        const string CountString = "Count";
        const string IndexerName = "Item[]";
        const string KeysName = "Keys";
        const string ValuesName = "Values";

        IDictionary<TKey, TValue> _dictionary;

        protected IDictionary<TKey, TValue> Dictionary
            => _dictionary;

        public ObservableDictionary()
            => _dictionary = new Dictionary<TKey, TValue>();

        public ObservableDictionary(IDictionary<TKey, TValue> dictionary)
        {
            _dictionary = new Dictionary<TKey, TValue>(dictionary);
        }

        public ObservableDictionary(IEqualityComparer<TKey> comparer)
        {
            _dictionary = new Dictionary<TKey, TValue>(comparer);
        }

        public ObservableDictionary(int capacity)
        {
            _dictionary = new Dictionary<TKey, TValue>(capacity);
        }

        public ObservableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
        {
            _dictionary = new Dictionary<TKey, TValue>(dictionary, comparer);
        }

        public ObservableDictionary(int capacity, IEqualityComparer<TKey> comparer)
        {
            _dictionary = new Dictionary<TKey, TValue>(capacity, comparer);
        }

        #region IDictionary<TKey,TValue> Members

        public void Add(TKey key, TValue value)
        {
            Insert(key, value, true);
        }

        public bool ContainsKey(TKey key)
            => Dictionary.ContainsKey(key);

        public ICollection<TKey> Keys
            => Dictionary.Keys;

        public bool Remove(TKey key)
        {
            if (EqualityComparer<TKey>.Default.Equals(key, default(TKey)))
                throw new ArgumentNullException(nameof(key));

            Dictionary.TryGetValue(key, out TValue value);
            var removed = Dictionary.Remove(key);
            if (removed)
            {
                OnCollectionChanged(NotifyCollectionChangedAction.Remove, new KeyValuePair<TKey, TValue>(key, value));
            }
            return removed;
        }

        public bool TryGetValue(TKey key, out TValue value)
            => Dictionary.TryGetValue(key, out value);

        public ICollection<TValue> Values
            => Dictionary.Values;

        public TValue this[TKey key]
        {
            get => Dictionary.ContainsKey(key) ? Dictionary[key] : default(TValue);
            set => Insert(key, value, false);
        }

        #endregion IDictionary<TKey,TValue> Members

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Insert(item.Key, item.Value, true);
        }

        public void Clear()
        {
            if (Dictionary.Count > 0)
            {
                Dictionary.Clear();
                OnCollectionChanged();
            }
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return Dictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            Dictionary.CopyTo(array, arrayIndex);
        }

        public int Count
            => Dictionary.Count;

        public bool IsReadOnly
            => Dictionary.IsReadOnly;

        public bool Remove(KeyValuePair<TKey, TValue> item)
            => Remove(item.Key);

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
            => Dictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => ((IEnumerable)Dictionary).GetEnumerator();

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        public void AddRange(IDictionary<TKey, TValue> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            if (items.Count > 0)
            {
                if (Dictionary.Count > 0)
                {
                    if (items.Keys.Any((k) => Dictionary.ContainsKey(k)))
                        throw new ArgumentException("An item with the same key has already been added.");

                    foreach (KeyValuePair<TKey, TValue> item in items)
                    {
                        Dictionary.Add(item);
                    }
                }
                else
                {
                    _dictionary = new Dictionary<TKey, TValue>(items);
                }

                OnCollectionChanged(NotifyCollectionChangedAction.Add, items.ToList());
            }
        }

        void Insert(TKey key, TValue value, bool add)
        {
            if (EqualityComparer<TKey>.Default.Equals(key, default(TKey)))
                throw new ArgumentNullException(nameof(key));

            if (Dictionary.TryGetValue(key, out TValue item))
            {
                if (add)
                    throw new ArgumentException("An item with the same key has already been added.");

                if (Equals(item, value))
                    return;

                Dictionary[key] = value;

                OnCollectionChanged(NotifyCollectionChangedAction.Replace, new KeyValuePair<TKey, TValue>(key, value), new KeyValuePair<TKey, TValue>(key, item));
            }
            else
            {
                Dictionary[key] = value;

                OnCollectionChanged(NotifyCollectionChangedAction.Add, new KeyValuePair<TKey, TValue>(key, value));
            }
        }

        void OnPropertyChanged()
        {
            OnPropertyChanged(CountString);
            OnPropertyChanged(IndexerName);
            OnPropertyChanged(KeysName);
            OnPropertyChanged(ValuesName);
        }

        protected virtual void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        void OnCollectionChanged()
        {
            OnPropertyChanged();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        void OnCollectionChanged(NotifyCollectionChangedAction action, KeyValuePair<TKey, TValue> changedItem)
        {
            OnPropertyChanged();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, changedItem));
        }

        void OnCollectionChanged(NotifyCollectionChangedAction action, KeyValuePair<TKey, TValue> newItem, KeyValuePair<TKey, TValue> oldItem)
        {
            OnPropertyChanged();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, newItem, oldItem));
        }

        void OnCollectionChanged(NotifyCollectionChangedAction action, IList newItems)
        {
            OnPropertyChanged();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, newItems));
        }

        public static implicit operator ObservableDictionary<TKey, TValue>(ObservableDictionary<string, string> v)
        {
            throw new NotImplementedException();
        }
    }
}