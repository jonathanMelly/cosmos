using System.Collections.Generic;

namespace lib.data
{
    public delegate void DataUpdatedHandler();

    public interface IObservableDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey,TValue>>,IDictionary<TKey,TValue>
    {
        event DataUpdatedHandler DataUpdated;

        int LongestName { get; }
    }

}