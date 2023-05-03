using System;
using System.Collections.Generic;
using System.Linq;

namespace CityPop.Core.ListSynchronizer
{
    public class ListSynchronizer<TKey, T1, T2>
    {
        T1[] _buffer = Array.Empty<T1>();
        readonly Func<T1, TKey> _type1Converter;
        readonly Func<T2, TKey> _type2Converter;
        readonly Func<T2, int, T1> _create;
        readonly Action<T1, int> _remove;
        readonly Action<T1, T2, int, int> _update;
        readonly Dictionary<TKey, (int From, int To)> _indexChanges = new();

        public ListSynchronizer(Func<T1, TKey> type1Converter, Func<T2, TKey> type2Converter, Func<T2, int, T1> create, Action<T1, int> remove, Action<T1, T2, int, int> update)
        {
            _type1Converter = type1Converter;
            _type2Converter = type2Converter;
            _create = create;
            _remove = remove;
            _update = update;
        }

        public void Synchronize(List<T1> listToSynchronize, IList<T2> syncWith)
        {
            _indexChanges.Clear();

            for (var i = 0; i < listToSynchronize.Count; ++i)
            {
                var key = _type1Converter(listToSynchronize[i]);
                if (!_indexChanges.TryGetValue(key, out var indexChange))
                    indexChange = (-1, -1);

                indexChange.From = i;
                _indexChanges[key] = indexChange;
            }

            for (var i = 0; i < syncWith.Count; ++i)
            {
                var key = _type2Converter(syncWith[i]);
                if (!_indexChanges.TryGetValue(key, out var indexChange))
                    indexChange = (-1, -1);

                indexChange.To = i;
                _indexChanges[key] = indexChange;
            }

            if (_buffer.Length < syncWith.Count)
                _buffer = new T1[syncWith.Count];

            foreach (var indexChange in _indexChanges.Values)
            {
                // Add / Create
                if (indexChange.From == -1)
                {
                    _buffer[indexChange.To] = _create(syncWith[indexChange.To], indexChange.To);
                }
                // Remove
                else if (indexChange.To == -1)
                {
                    _remove(listToSynchronize[indexChange.From], indexChange.From);
                    listToSynchronize[indexChange.From] = default;
                }
                // Update
                else
                {
                    _update(listToSynchronize[indexChange.From], syncWith[indexChange.To], indexChange.From, indexChange.To);
                    _buffer[indexChange.To] = listToSynchronize[indexChange.From];
                }
            }

            listToSynchronize.Clear();
            listToSynchronize.AddRange(_buffer.Take(syncWith.Count));
        }
    }
}