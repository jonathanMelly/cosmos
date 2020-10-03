using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace lib.data
{
    public class OwnThreadNotifiableDictionary<TKey, TValue> : IObservableDictionary<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> content = new Dictionary<TKey, TValue>();
        private int longestName=0;

        public bool TryGetValue(TKey key, out TValue value)
        {
            return content.TryGetValue(key, out value);
        }

        public TValue this[TKey key]
        {
            get => content[key];
            set
            {
                var size = (key.ToString()??"").Length;
                if (size  > longestName)
                {
                    longestName = size;
                }
                content[key] = value;
                Notifiy();
            }
        }

        public ICollection<TKey> Keys => content.Keys;
        public ICollection<TValue> Values => content.Values;

        public void Add(TKey key, TValue value)
        {
            content.Add(key, value);
            Notifiy();
        }

        public bool ContainsKey(TKey key)
        {
            return content.ContainsKey(key);
        }

        public bool Remove(TKey key)
        {
            var result = content.Remove(key);
            if (result)
            {
                Notifiy();
            }

            return result;
        }

        public void Clear()
        {
            content.Clear();
            Notifiy();
        }

        public int Count => content.Count;
        public bool IsReadOnly => false;


        private void Notifiy()
        {
            ThreadPool.QueueUserWorkItem(state => DataUpdated?.Invoke());
        }


        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return content.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
            Notifiy();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return content.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            content.CopyTo(array, arrayIndex);
            Notifiy();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            var result = Remove(item.Key);
            if (result)
            {
                Notifiy();
            }

            return result;

        }

        public event DataUpdatedHandler DataUpdated;
        public int LongestName => longestName;
    }
}