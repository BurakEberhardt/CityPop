using CityPop.Character;
using CityPop.CharacterCreator.Configurations;
using CityPop.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace CityPop.CharacterCreator.Views
{
    public class CharacterCreatorMenu : View
    {
        [SerializeField] CharacterCreatorConfiguration _configuration;
        [SerializeField] CharacterVisualsView _characterView;
        [FormerlySerializedAs("_characterCreatorBodyUiView")] [SerializeField] CharacterCreatorBodySelectorUiView _characterCreatorBodySelectorUiView;
        [FormerlySerializedAs("_characterCreatorHairUiView")] [SerializeField] CharacterCreatorHairSelectorUiView _characterCreatorHairSelectorUiView;
        [FormerlySerializedAs("_characterCreatorFaceUiView")] [SerializeField] CharacterCreatorFaceSelectorUiView _characterCreatorFaceSelectorUiView;
        CharacterVisualsData _characterData;

        void Awake()
        {
            _characterData = new CharacterVisualsData();
            _characterView.CharacterVisualsData = _characterData;
            
            _characterCreatorBodySelectorUiView.BodyVisualsData = _characterData.Body;
            _characterCreatorBodySelectorUiView.CharacterCreatorBodyConfiguration = _configuration.Body;
            
            _characterCreatorHairSelectorUiView.HairVisualsData = _characterData.Hair;
            _characterCreatorHairSelectorUiView.CharacterCreatorHairConfiguration = _configuration.Hair;
            
            _characterCreatorFaceSelectorUiView.FaceVisualsData = _characterData.Face;
            _characterCreatorFaceSelectorUiView.CharacterCreatorFaceConfiguration = _configuration.Face;
        }
    }
}