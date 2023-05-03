using System.Collections;
using System.Collections.Generic;

namespace Zen.CodeGeneration
{
    public class ListData<T> : IList<T>
    {
        readonly IList<T> _list;

        public ListData() => _list = new List<T>();
        public ListData(int capacity) => _list = new List<T>(capacity);
        public ListData(IEnumerable<T> collection) => _list = new List<T>(collection);

        public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public delegate void OnAdded(T item, int index);

        public event OnAdded EventAdded;

        public void Add(T item)
        {
            var index = _list.Count;
            _list.Add(item);

            EventAdded?.Invoke(item, index);
        }

        public delegate void OnClear();

        public event OnClear EventClear;

        public void Clear()
        {
            _list.Clear();

            EventClear?.Invoke();
        }

        public bool Contains(T item) => _list.Contains(item);
        public void CopyTo(T[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);

        public delegate void OnRemoved(T item, int index);

        public event OnRemoved EventRemoved;

        public bool Remove(T item)
        {
            var index = _list.IndexOf(item);
            if (index < 0)
                return false;

            _list.RemoveAt(index);

            EventRemoved?.Invoke(item, index);
            return true;
        }

        public int Count => _list.Count;
        public bool IsReadOnly => _list.IsReadOnly;
        public int IndexOf(T item) => _list.IndexOf(item);

        public void Insert(int index, T item)
        {
            _list.Insert(index, item);

            EventAdded?.Invoke(item, index);
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _list.Count)
                return;

            var item = _list[index];
            _list.RemoveAt(index);
            
            EventRemoved?.Invoke(item, index);
        }

        public T this[int index]
        {
            get => _list[index];
            set => _list[index] = value;
        }
    }
}