using CityPop.Character;
using CityPop.CharacterCreator.Configurations;
using UnityEngine;
using Zen.Core.View;

namespace CityPop.CharacterCreator.Views
{
    public class CharacterCreatorMenu : View
    {
        [SerializeField] CharacterCreatorConfiguration _configuration;
        [SerializeField] CharacterVisualsView _characterView;
        [SerializeField] CharacterVisualsView[] _characterViews;
        [SerializeField] CharacterCreatorBodySelectorUiView _characterCreatorBodySelectorUiView;
        [SerializeField] CharacterCreatorHairSelectorUiView _characterCreatorHairSelectorUiView;
        [SerializeField] CharacterCreatorFaceSelectorUiView _characterCreatorFaceSelectorUiView;
        CharacterVisualsData _characterData;

        void Awake()
        {
            _characterData = new CharacterVisualsData();
            _characterView.CharacterVisualsData = _characterData;
            foreach (var characterVisualsView in _characterViews)
                characterVisualsView.CharacterVisualsData = _characterData;
            
            _characterCreatorBodySelectorUiView.BodyVisualsData = _characterData.Body;
            _characterCreatorBodySelectorUiView.CharacterCreatorBodyConfiguration = _configuration.Body;
            
            _characterCreatorHairSelectorUiView.HairVisualsData = _characterData.Hair;
            _characterCreatorHairSelectorUiView.CharacterCreatorHairConfiguration = _configuration.Hair;
            
            _characterCreatorFaceSelectorUiView.FaceVisualsData = _characterData.Face;
            _characterCreatorFaceSelectorUiView.CharacterCreatorFaceConfiguration = _configuration.Face;
        }
    }
}