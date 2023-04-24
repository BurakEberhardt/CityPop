using CityPop.Character;
using CityPop.CharacterCreator.Configurations;
using CityPop.Core;
using UnityEngine;

namespace CityPop.CharacterCreator.Views
{
    public class CharacterCreatorMenu : View
    {
        [SerializeField] CharacterCreatorConfiguration _configuration;
        [SerializeField] CharacterVisualsView _characterView;
        [SerializeField] CharacterCreatorBodyUiView _characterCreatorBodyUiView;
        [SerializeField] CharacterCreatorHairUiView _characterCreatorHairUiView;
        [SerializeField] CharacterCreatorFaceUiView _characterCreatorFaceUiView;
        CharacterVisualsData _characterData;

        void Awake()
        {
            _characterData = new CharacterVisualsData();
            _characterView.CharacterVisualsData = _characterData;
            
            _characterCreatorBodyUiView.BodyVisualsData = _characterData.Body;
            _characterCreatorBodyUiView.CharacterCreatorBodyConfiguration = _configuration.Body;
            
            _characterCreatorHairUiView.HairVisualsData = _characterData.Hair;
            _characterCreatorHairUiView.CharacterCreatorHairConfiguration = _configuration.Hair;
            
            _characterCreatorFaceUiView.FaceVisualsData = _characterData.Face;
            _characterCreatorFaceUiView.CharacterCreatorFaceConfiguration = _configuration.Face;
        }
    }
}