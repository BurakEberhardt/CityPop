using System;
using System.Collections.Generic;
using UnityEngine;

namespace CityPop.Character
{
    public enum BodyType : byte
    {
    }

    
    [Serializable]
    public class BodyVisualsData
    {
        public interface IAddedListener { void OnBodyVisualsData(BodyVisualsData data); }
        public interface IRemovedListener { void OnBodyVisualsDataRemoved(); }
        
        [SerializeField] BodyType _type;
        public BodyType Type
        {
            get => _type;
            set
            {
                _type = value;
                
                foreach (var listener in _typeListeners) 
                    listener.OnBodyType(value);
            }
        }
        
        public interface ITypeListener { void OnBodyType(BodyType type); }
        List<ITypeListener> _typeListeners = new();
        public void AddTypeListener(ITypeListener listener) => _typeListeners.Add(listener);
        public void RemoveTypeListener(ITypeListener listener) => _typeListeners.Remove(listener);
        
        
        [SerializeField] Color32 _color;
        public Color32 Color
        {
            get => _color;
            set
            {
                _color = value;
                
                foreach (var listener in _colorListeners) 
                    listener.OnBodyColor(value);
            }
        }
        
        public interface IColorListener { void OnBodyColor(Color32 color); }
        List<IColorListener> _colorListeners = new();
        public void AddColorListener(IColorListener listener) => _colorListeners.Add(listener);
        public void RemoveColorListener(IColorListener listener) => _colorListeners.Remove(listener);
    }
}