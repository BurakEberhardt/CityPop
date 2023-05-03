namespace Zen.CodeGeneration
{
    public class ListEvents<T>
    {
        ListData<T> _targetData;
        public ListData<T> TargetData
        {
            set
            {
                if (_targetData != null)
                {
                    _targetData.EventAdded -= OnAdded;
                    _targetData.EventRemoved -= OnRemoved;
                    _targetData.EventClear -= OnClear;
                }

                _targetData = value;
                
                if (_targetData != null)
                {
                    _targetData.EventAdded += OnAdded;
                    _targetData.EventRemoved += OnRemoved;
                    _targetData.EventClear += OnClear;
                }
            }
        }
        
        void OnAdded(T item, int index)
        {
            foreach (var listener in _addedListeners)
                listener.OnAdded(item, index);
        }

        void OnRemoved(T item, int index)
        {
            foreach (var listener in _removedListeners)
                listener.OnRemoved(item, index);
        }

        void OnClear()
        {
            foreach (var listener in _clearListeners)
                listener.OnClear();
        }

        public interface IAddedListener
        {
            void OnAdded(T element, int index);
        }

        readonly System.Collections.Generic.HashSet<IAddedListener> _addedListeners = new();
        public void AddAddedListener(IAddedListener listener) => _addedListeners.Add(listener);
        public void RemoveAddedListener(IAddedListener listener) => _addedListeners.Remove(listener);

        public interface IRemovedListener
        {
            void OnRemoved(T element, int index);
        }

        readonly System.Collections.Generic.HashSet<IRemovedListener> _removedListeners = new();

        public void AddRemovedListener(IRemovedListener listener) => _removedListeners.Add(listener);

        public void RemoveRemovedListener(IRemovedListener listener) => _removedListeners.Remove(listener);

        public interface IClearListener
        {
            void OnClear();
        }

        readonly System.Collections.Generic.HashSet<IClearListener> _clearListeners = new();
        public void AddClearListener(IClearListener listener) => _clearListeners.Add(listener);
        public void RemoveClearListener(IClearListener listener) => _clearListeners.Remove(listener);
    }
}