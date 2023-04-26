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
      
        void CharacterCreatorFaceConfiguration.IAddedListener.OnAdded(CharacterCreatorFaceConfiguration characterCreatorFaceConfiguration)
        {
            _prevFaceButton.onClick.AddListener(PrevFace);
            _nextFaceButton.onClick.AddListener(NextFace);
            _faceIndex = 0;
            UpdateFace();
        }

        void CharacterCreatorFaceConfiguration.IRemovedListener.OnRemoved()
        {
            _prevFaceButton.onClick.RemoveListener(PrevFace);
            _nextFaceButton.onClick.RemoveListener(NextFace);
        }

        async void FaceVisualsData.ITypeListener.OnType(FaceType type)
        {
            var faceAsset = CharacterVisualsAddressables.GetFaceVisualsConfiguration(type);
            var configuration = await faceAsset.Task;

            _iconImage.sprite = configuration.Sprite;
        }

        void FaceVisualsData.IColorListener.OnColor(Color32 color)
        {
            _iconImage.color = color;
        }
    }
}