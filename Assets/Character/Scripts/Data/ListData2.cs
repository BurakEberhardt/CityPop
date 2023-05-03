// using System.Collections;
// using System.Collections.Generic;
//
// namespace CityPop.Character
// {
//     public class ListData<T> : IList<T>
//     {
//         readonly IList<T> _list;
//         
//         public ListData() => _list = new List<T>();
//         public ListData(int capacity) => _list = new List<T>(capacity);
//         public ListData(IEnumerable<T> collection) => _list = new List<T>(collection);
//
//         public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();
//         IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
//
//         public interface IAddedListener
//         {
//             void OnAdded(T element, int index);
//         }
//
//         readonly System.Collections.Generic.HashSet<IAddedListener> _addedListeners = new();
//         public void AddAddedListener(IAddedListener listener) => _addedListeners.Add(listener);
//         public void RemoveAddedListener(IAddedListener listener) => _addedListeners.Remove(listener);
//
//         public void Add(T item)
//         {
//             var index = _list.Count;
//             _list.Add(item);
//
//             foreach (var listener in _addedListeners)
//                 listener.OnAdded(item, index);
//         }
//         
//         public interface IClearListener
//         {
//             void OnClear();
//         }
//
//         readonly System.Collections.Generic.HashSet<IClearListener> _clearListeners = new();
//         public void AddClearListener(IClearListener listener) => _clearListeners.Add(listener);
//         public void RemoveClearListener(IClearListener listener) => _clearListeners.Remove(listener);
//
//         public void Clear()
//         {
//             _list.Clear();
//             
//             foreach (var listener in _clearListeners)
//                 listener.OnClear();
//         }
//
//         public bool Contains(T item) => _list.Contains(item);
//         public void CopyTo(T[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);
//
//         public interface IRemovedListener
//         {
//             void OnRemoved(T element, int index);
//         }
//
//         readonly System.Collections.Generic.HashSet<IRemovedListener> _removedListeners = new();
//         public void AddRemovedListener(IRemovedListener listener) => _removedListeners.Add(listener);
//         public void RemoveRemovedListener(IRemovedListener listener) => _removedListeners.Remove(listener);
//         
//         public bool Remove(T item)
//         {
//             var index = _list.IndexOf(item);
//             if (index < 0)
//                 return false;
//             
//             _list.RemoveAt(index);
//             
//             foreach (var listener in _removedListeners)
//                 listener.OnRemoved(item, index);
//
//             return true;
//         }
//
//         public int Count => _list.Count;
//         public bool IsReadOnly => _list.IsReadOnly;
//         public int IndexOf(T item) => _list.IndexOf(item);
//
//         public void Insert(int index, T item)
//         {
//             _list.Insert(index, item);
//
//             foreach (var listener in _addedListeners)
//                 listener.OnAdded(item, index);
//         }
//
//         public void RemoveAt(int index)
//         {
//             if(index < 0 || index >= _list.Count)
//                 return;
//
//             var item = _list[index];
//             _list.RemoveAt(index);
//             
//             foreach (var listener in _removedListeners)
//                 listener.OnRemoved(item, index);
//         }
//
//         public T this[int index]
//         {
//             get => _list[index];
//             set => _list[index] = value;
//         }
//     }
// }