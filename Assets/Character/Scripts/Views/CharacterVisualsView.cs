using CityPop.Core;
using CityPop.Core.Shared.Attributes;
using UnityEngine;

namespace CityPop.Character
{
    [DataBinding(typeof(CharacterVisualsData))]
    public partial class CharacterVisualsView : View
        , CharacterVisualsData.IAddedListener
        , CharacterVisualsData.IRemovedListener
    {
        [SerializeField] BodyVisualsView _bodyVisuals;
        [SerializeField] HairVisualsView _hairVisuals;
        [SerializeField] FaceVisualsView _faceVisuals;

        protected CharacterVisualsData _characterVisualsData;
        public CharacterVisualsData CharacterVisualsData
        {
            get => _characterVisualsData;
            set
            {
                if (_characterVisualsData != null)
                {
                    (this as CharacterVisualsData.IRemovedListener).OnRemoved();
                }

                _characterVisualsData = value;

                if (_characterVisualsData != null)
                {
                    (this as CharacterVisualsData.IAddedListener).OnAdded(_characterVisualsData);
                }
            }
        }

        void CharacterVisualsData.IAddedListener.OnAdded(CharacterVisualsData characterVisualsData)
        {
            _bodyVisuals.BodyVisualsData = characterVisualsData.Body;
            _hairVisuals.HairVisualsData = characterVisualsData.Hair;
            _faceVisuals.FaceVisualsData = characterVisualsData.Face;
        }

        void CharacterVisualsData.IRemovedListener.OnRemoved()
        {
            _bodyVisuals.BodyVisualsData = null;
            _hairVisuals.HairVisualsData = null;
            _faceVisuals.FaceVisualsData = null;
        }
    }
}