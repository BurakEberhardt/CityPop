using System;
using CharacterCreator.Data;
using CityPop.Character;
using CityPop.CharacterCreator.Configurations;
using Ui.ColorPicker.Predefined.Data;
using Ui.ColorPicker.Predefined.Views;
using UnityEngine;
using UnityEngine.UI;
using Zen.Core.View;
using Zen.CodeGeneration.DataBinding.Attributes;

namespace CityPop.CharacterCreator.Views
{
    [DataBinding(typeof(FaceVisualsData))]
    [DataBinding(typeof(CharacterCreatorFaceConfiguration))]
    [DataBinding(typeof(CharacterCreatorPartSelectorData))]
    [DataBinding(typeof(PredefinedColorPickerData))]
    public partial class CharacterCreatorFaceSelectorUiView : View
        , FaceVisualsData.ITypeListener
        , FaceVisualsData.IColorListener
        , CharacterCreatorFaceConfiguration.IAddedListener
        , CharacterCreatorPartSelectorData.IIndexListener
        , PredefinedColorPickerData.IIndexListener
    {
        [SerializeField] CharacterCreatorPartSelectorUiView _partSelectorUi;
        [SerializeField] PredefinedColorPickerUiView _colorPicker;
        [SerializeField] Image _iconImage;

        void CharacterCreatorFaceConfiguration.IAddedListener.OnAdded(CharacterCreatorFaceConfiguration characterCreatorFaceConfiguration)
        {
            CharacterCreatorPartSelectorData = _partSelectorUi.CharacterCreatorPartSelectorData = new CharacterCreatorPartSelectorData()
            {
                Index = Math.Max(Array.IndexOf(characterCreatorFaceConfiguration.Types, FaceVisualsData.Type), 0),
                Count = characterCreatorFaceConfiguration.Types.Length
            };

            PredefinedColorPickerData = _colorPicker.PredefinedColorPickerData = new PredefinedColorPickerData()
            {
                Index = Math.Max(Array.IndexOf(characterCreatorFaceConfiguration.Colors, FaceVisualsData.Color), 0),
                Colors = characterCreatorFaceConfiguration.Colors
            };
        }

        [UpdateOnInitialize]
        void CharacterCreatorPartSelectorData.IIndexListener.OnIndex(int index)
        {
            _faceVisualsData.Type = _characterCreatorFaceConfiguration.Types[CharacterCreatorPartSelectorData.Index];
        }

        [UpdateOnInitialize]
        void PredefinedColorPickerData.IIndexListener.OnIndex(int index)
        {
            _faceVisualsData.Color = _characterCreatorFaceConfiguration.Colors[index];
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