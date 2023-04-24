using CityPop.Character;
using CityPop.CharacterCreator.Configurations;
using CityPop.Core;
using CityPop.Core.Shared.Attributes;
using Core.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace CityPop.CharacterCreator.Views
{
    [DataBinding(typeof(FaceVisualsData))]
    [DataBinding(typeof(CharacterCreatorFaceConfiguration))]
    public partial class CharacterCreatorFaceUiView : View
        , FaceVisualsData.ITypeListener
        , FaceVisualsData.IColorListener
        , CharacterCreatorFaceConfiguration.IAddedListener
        , CharacterCreatorFaceConfiguration.IRemovedListener
    {
        [SerializeField] Image _iconImage;
        [SerializeField] Button _nextFaceButton;
        [SerializeField] Button _prevFaceButton;
        int _faceIndex;

        FaceVisualsData _faceVisualsData;
        public FaceVisualsData FaceVisualsData
        {
            get => _faceVisualsData;
            set
            {
                if (_faceVisualsData != null)
                {
                    _faceVisualsData.RemoveTypeListener(this);
                    _faceVisualsData.RemoveColorListener(this);
                }

                _faceVisualsData = value;

                if (_faceVisualsData != null)
                {
                    _faceVisualsData.AddTypeListener(this);
                    _faceVisualsData.AddColorListener(this);
                }
            }
        }
        
        CharacterCreatorFaceConfiguration _characterCreatorFaceConfiguration;
        public CharacterCreatorFaceConfiguration CharacterCreatorFaceConfiguration
        {
            get => _characterCreatorFaceConfiguration;
            set
            {
                if (_characterCreatorFaceConfiguration != null)
                {
                    OnCharacterCreatorFaceConfigurationRemoved();
                }

                _characterCreatorFaceConfiguration = value;

                if (_characterCreatorFaceConfiguration != null)
                {
                    OnCharacterCreatorFaceConfiguration(_characterCreatorFaceConfiguration);
                }
            }
        }

        void PrevFace()
        {
            _faceIndex = (--_faceIndex).Mod(_characterCreatorFaceConfiguration.Types.Length);
            UpdateFace();
        }

        void NextFace()
        {
            _faceIndex = (++_faceIndex).Mod(_characterCreatorFaceConfiguration.Types.Length);
            UpdateFace();
        }

        void UpdateFace()
        {
            _faceVisualsData.Type = _characterCreatorFaceConfiguration.Types[_faceIndex];
        }
      
        public async void OnFaceType(FaceType type)
        {
            var faceAsset = CharacterVisualsAddressables.GetFaceVisualsConfiguration(type);
            var configuration = await faceAsset.Task;

            _iconImage.sprite = configuration.Sprite;
        }


        public void OnFaceColor(Color32 color)
        {
            _iconImage.color = color;
        }

        public void OnCharacterCreatorFaceConfiguration(CharacterCreatorFaceConfiguration data)
        {
            _prevFaceButton.onClick.AddListener(PrevFace);
            _nextFaceButton.onClick.AddListener(NextFace);
            _faceIndex = 0;
            UpdateFace();
        }

        public void OnCharacterCreatorFaceConfigurationRemoved()
        {
            _prevFaceButton.onClick.RemoveListener(PrevFace);
            _nextFaceButton.onClick.RemoveListener(NextFace);
        }
    }
}