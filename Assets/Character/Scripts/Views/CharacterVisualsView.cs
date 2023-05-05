using UnityEngine;
using Zen.Core.View;
using Zen.CodeGeneration.DataBinding.Attributes;
using Zen.CodeGeneration.UnityMethods.Attributes;

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