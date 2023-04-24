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
                    OnCharacterVisualsDataRemoved();
                }

                _characterVisualsData = value;

                if (_characterVisualsData != null)
                {
                    OnCharacterVisualsData(_characterVisualsData);
                }
            }
        }

        public void OnCharacterVisualsData(CharacterVisualsData data)
        {
            _bodyVisuals.BodyVisualsData = data.Body;
            _hairVisuals.HairVisualsData = data.Hair;
            _faceVisuals.FaceVisualsData = data.Face;
        }

        public void OnCharacterVisualsDataRemoved()
        {
            _bodyVisuals.BodyVisualsData = null;
            _hairVisuals.HairVisualsData = null;
            _faceVisuals.FaceVisualsData = null;
        }
    }
}