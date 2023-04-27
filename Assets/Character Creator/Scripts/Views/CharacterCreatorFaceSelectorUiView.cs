using CharacterCreator.Data;
using CityPop.Character;
using CityPop.CharacterCreator.Configurations;
using CityPop.Core;
using CityPop.Core.Shared.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace CityPop.CharacterCreator.Views
{
    [DataBinding(typeof(FaceVisualsData))]
    [DataBinding(typeof(CharacterCreatorFaceConfiguration))]
    [DataBinding(typeof(CharacterCreatorPartSelectorData))]
    public partial class CharacterCreatorFaceSelectorUiView : View
        , FaceVisualsData.ITypeListener
        , FaceVisualsData.IColorListener
        , CharacterCreatorFaceConfiguration.IAddedListener
        , CharacterCreatorPartSelectorData.IIndexListener
    {
        [SerializeField] CharacterCreatorPartSelectorUiView _partSelectorUi;
        [SerializeField] Image _iconImage;

        void CharacterCreatorFaceConfiguration.IAddedListener.OnAdded(CharacterCreatorFaceConfiguration characterCreatorFaceConfiguration)
        {
            CharacterCreatorPartSelectorData = _partSelectorUi.CharacterCreatorPartSelectorData = new CharacterCreatorPartSelectorData()
            {
                Index = 0,
                Count = characterCreatorFaceConfiguration.Types.Length
            };
        }

        [UpdateOnInitialize]
        void CharacterCreatorPartSelectorData.IIndexListener.OnIndex(int index)
        {
            _faceVisualsData.Type = _characterCreatorFaceConfiguration.Types[CharacterCreatorPartSelectorData.Index];
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