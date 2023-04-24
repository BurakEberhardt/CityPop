using UnityEngine;

namespace CityPop.Core
{
    public class View<T> : View
    {
        protected T _data;
        public T Data
        {
            get => _data;
            set
            {
                _data = value;
            }
        }
    }
}