using System;
using System.Collections.Generic;
using UnityEngine;

namespace CityPop.Character
{
    public enum FaceType : byte
    {
    }

    
    [Serializable]
    public class FaceVisualsData
    {
        public interface IAddedListener { void OnFaceVisualsData(FaceVisualsData data); }
        public interface IRemovedListener { void OnFaceVisualsDataRemoved(); }
        
        [SerializeField] FaceType _type;
        public FaceType Type
        {
            get => _type;
            set
            {
                _type = value;
                
                foreach (var listener in _typeListeners) 
                    listener.OnFaceType(value);
            }
        }
        
        public interface ITypeListener { void OnFaceType(FaceType type); }
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
                    listener.OnFaceColor(value);
            }
        }
        
        public interface IColorListener { void OnFaceColor(Color32 color); }
        List<IColorListener> _colorListeners = new();
        public void AddColorListener(IColorListener listener) => _colorListeners.Add(listener);
        public void RemoveColorListener(IColorListener listener) => _colorListeners.Remove(listener);
    }
}