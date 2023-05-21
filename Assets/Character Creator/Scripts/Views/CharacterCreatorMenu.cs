using System;
using CityPop.Character;
using CityPop.CharacterCreator.Configurations;
using CityPop.CharacterToTexture.Views;
using TMPro;
using UnityEngine;
using Zen.CodeGeneration.DataBinding.Attributes;
using Zen.Core.View;
using Zen.Ui;

namespace CityPop.CharacterCreator.Views
{
    [DataBinding(typeof(CharacterData))]
    public partial class CharacterCreatorMenu : View
        , CharacterData.IAddedListener
        , CharacterData.IRemovedListener
    {
        [SerializeField] CharacterCreatorConfiguration _configuration;
        [SerializeField] CharacterSpriteView _characterView;
        [SerializeField] CharacterCreatorBodySelectorUiView _characterCreatorBodySelectorUiView;
        [SerializeField] CharacterCreatorHairSelectorUiView _characterCreatorHairSelectorUiView;
        [SerializeField] CharacterCreatorFaceSelectorUiView _characterCreatorFaceSelectorUiView;
        [SerializeField] TMP_InputField _nameLabel;
        [SerializeField] Button _createButton;

        void CharacterData.IAddedListener.OnAdded(CharacterData characterData)
        {
            _characterView.CharacterVisualsData = _characterData.Visuals;
            
            _characterCreatorBodySelectorUiView.BodyVisualsData = _characterData.Visuals.Body;
            _characterCreatorBodySelectorUiView.CharacterCreatorBodyConfiguration = _configuration.Body;
             
            _characterCreatorHairSelectorUiView.HairVisualsData = _characterData.Visuals.Hair;
            _characterCreatorHairSelectorUiView.CharacterCreatorHairConfiguration = _configuration.Hair;
            
            _characterCreatorFaceSelectorUiView.FaceVisualsData = _characterData.Visuals.Face;
            _characterCreatorFaceSelectorUiView.CharacterCreatorFaceConfiguration = _configuration.Face;

            _nameLabel.onValueChanged.AddListener(OnNameChanged);
            _nameLabel.text = characterData.Name;
            _createButton.onClick.AddListener(OnCreate);
        }

        void CharacterData.IRemovedListener.OnRemoved()
        {
            _characterView.CharacterVisualsData = null;
            
            _characterCreatorBodySelectorUiView.BodyVisualsData = null;
            _characterCreatorBodySelectorUiView.CharacterCreatorBodyConfiguration = null;
            
            _characterCreatorHairSelectorUiView.HairVisualsData = null;
            _characterCreatorHairSelectorUiView.CharacterCreatorHairConfiguration = null;
            
            _characterCreatorFaceSelectorUiView.FaceVisualsData = null;
            _characterCreatorFaceSelectorUiView.CharacterCreatorFaceConfiguration = null;
            
            _nameLabel.onValueChanged.RemoveListener(OnNameChanged);
            _nameLabel.text = string.Empty;
            _createButton.onClick.RemoveListener(OnCreate);
        }

        public event Action<CharacterData> EventCreate;

        void OnNameChanged(string name)
        {
            CharacterData.Name = name;
        }

        void OnCreate()
        {
            EventCreate?.Invoke(CharacterData);
        }
    }
}