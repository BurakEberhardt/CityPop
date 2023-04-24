using System;
using System.Collections.Generic;
using UnityEngine;

namespace CityPop.Character
{
    public enum HairType : byte
    {
    }

    
    [Serializable]
    public class HairVisualsData
    {
        public interface IAddedListener { void OnHairVisualsData(HairVisualsData data); }
        public interface IRemovedListener { void OnHairVisualsDataRemoved(); }
        
        [SerializeField] HairType _type;
        public HairType Type
        {
            get => _type;
            set
            {
                _type = value;
                
                foreach (var listener in _typeListeners) 
                    listener.OnHairType(value);
            }
        }
        
        public interface ITypeListener { void OnHairType(HairType type); }
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
                    listener.OnHairColor(value);
            }
        }
        
        public interface IColorListener { void OnHairColor(Color32 color); }
        List<IColorListener> _colorListeners = new();
        public void AddColorListener(IColorListener listener) => _colorListeners.Add(listener);
        public void RemoveColorListener(IColorListener listener) => _colorListeners.Remove(listener);
    }
}