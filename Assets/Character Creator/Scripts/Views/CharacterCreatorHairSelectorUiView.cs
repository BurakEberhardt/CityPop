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
    [DataBinding(typeof(HairVisualsData))]
    [DataBinding(typeof(CharacterCreatorHairConfiguration))]
    [DataBinding(typeof(CharacterCreatorPartSelectorData))]
    [DataBinding(typeof(PredefinedColorPickerData))]
    public partial class CharacterCreatorHairSelectorUiView : View
        , HairVisualsData.ITypeListener
        , HairVisualsData.IColorListener
        , CharacterCreatorHairConfiguration.IAddedListener
        , CharacterCreatorPartSelectorData.IIndexListener
        , PredefinedColorPickerData.IIndexListener
    {
        [SerializeField] CharacterCreatorPartSelectorUiView _partSelectorUi;
        [SerializeField] PredefinedColorPickerUiView _colorPicker;
        [SerializeField] Image _iconImage;

        void CharacterCreatorHairConfiguration.IAddedListener.OnAdded(CharacterCreatorHairConfiguration characterCreatorHairConfiguration)
        {
            CharacterCreatorPartSelectorData = _partSelectorUi.CharacterCreatorPartSelectorData = new CharacterCreatorPartSelectorData()
            {
                Index = Math.Max(Array.IndexOf(characterCreatorHairConfiguration.Types, HairVisualsData.Type), 0),
                Count = characterCreatorHairConfiguration.Types.Length
            };

            PredefinedColorPickerData = _colorPicker.PredefinedColorPickerData = new PredefinedColorPickerData()
            {
                Index = Math.Max(Array.IndexOf(characterCreatorHairConfiguration.Colors, HairVisualsData.Color), 0),
                Colors = characterCreatorHairConfiguration.Colors
            };
        }

        [UpdateOnInitialize]
        void CharacterCreatorPartSelectorData.IIndexListener.OnIndex(int index)
        {
            _hairVisualsData.Type = _characterCreatorHairConfiguration.Types[CharacterCreatorPartSelectorData.Index];
        }

        [UpdateOnInitialize]
        void PredefinedColorPickerData.IIndexListener.OnIndex(int index)
        {
            _hairVisualsData.Color = _characterCreatorHairConfiguration.Colors[index];
        }

        async void HairVisualsData.ITypeListener.OnType(HairType type)
        {
            var hairAsset = CharacterVisualsAddressables.GetHairVisualsConfiguration(type);
            var configuration = await hairAsset.Task;

            _iconImage.sprite = configuration.Sprite;
        }

        void HairVisualsData.IColorListener.OnColor(Color32 color)
        {
            _iconImage.color = color;
        }
    }
}