using System.Collections.Generic;
using UnityEngine;

namespace CityPop.Character
{
    public class CharacterVisualsData
    {
        public interface IAddedListener { void OnCharacterVisualsData(CharacterVisualsData data); }
        public interface IRemovedListener { void OnCharacterVisualsDataRemoved(); }
        
        [SerializeField] BodyVisualsData _body = new();
        public BodyVisualsData Body
        {
            get => _body;
            set
            {
                _body = value;

                foreach (var listener in _bodyListeners) 
                    listener.OnBodyVisualsDataChanged(value);
            }
        }
        
        public interface IBodyListener { void OnBodyVisualsDataChanged(BodyVisualsData body); }
        List<IBodyListener> _bodyListeners = new();
        public void AddBodyListener(IBodyListener listener) => _bodyListeners.Add(listener);
        public void RemoveBodyListener(IBodyListener listener) => _bodyListeners.Remove(listener);
        
        [SerializeField] HairVisualsData _hair = new();
        public HairVisualsData Hair
        {
            get => _hair;
            set
            {
                _hair = value;

                foreach (var listener in _hairListeners) 
                    listener.OnHairVisualsDataChanged(value);
            }
        }
        
        public interface IHairListener { void OnHairVisualsDataChanged(HairVisualsData hair); }
        List<IHairListener> _hairListeners = new();
        public void AddHairListener(IHairListener listener) => _hairListeners.Add(listener);
        public void RemoveHairListener(IHairListener listener) => _hairListeners.Remove(listener);
        
        [SerializeField] FaceVisualsData _face = new();
        public FaceVisualsData Face
        {
            get => _face;
            set
            {
                _face = value;

                foreach (var listener in _faceListeners) 
                    listener.OnFaceVisualsDataChanged(value);
            }
        }
        
        public interface IFaceListener { void OnFaceVisualsDataChanged(FaceVisualsData face); }
        List<IFaceListener> _faceListeners = new();
        public void AddFaceListener(IFaceListener listener) => _faceListeners.Add(listener);
        public void RemoveFaceListener(IFaceListener listener) => _faceListeners.Remove(listener);
    }
}