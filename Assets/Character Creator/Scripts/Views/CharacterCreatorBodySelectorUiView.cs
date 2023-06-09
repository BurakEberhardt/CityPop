﻿using System;
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
    [DataBinding(typeof(BodyVisualsData))]
    [DataBinding(typeof(CharacterCreatorBodyConfiguration))]
    [DataBinding(typeof(CharacterCreatorPartSelectorData))]
    [DataBinding(typeof(PredefinedColorPickerData))]
    public partial class CharacterCreatorBodySelectorUiView : View
        , BodyVisualsData.ITypeListener
        , BodyVisualsData.IColorListener
        , CharacterCreatorBodyConfiguration.IAddedListener
        , CharacterCreatorPartSelectorData.IIndexListener
        , PredefinedColorPickerData.IIndexListener
    {
        [SerializeField] CharacterCreatorPartSelectorUiView _partSelectorUi;
        [SerializeField] PredefinedColorPickerUiView _colorPicker;
        [SerializeField] Image _iconImage;

        void CharacterCreatorBodyConfiguration.IAddedListener.OnAdded(CharacterCreatorBodyConfiguration characterCreatorBodyConfiguration)
        {
            CharacterCreatorPartSelectorData = _partSelectorUi.CharacterCreatorPartSelectorData = new CharacterCreatorPartSelectorData()
            {
                Index = Math.Max(Array.IndexOf(characterCreatorBodyConfiguration.Types, BodyVisualsData.Type), 0),
                Count = characterCreatorBodyConfiguration.Types.Length
            };

            PredefinedColorPickerData = _colorPicker.PredefinedColorPickerData = new PredefinedColorPickerData()
            {
                Index = Math.Max(Array.IndexOf(characterCreatorBodyConfiguration.Colors, BodyVisualsData.Color), 0),
                Colors = characterCreatorBodyConfiguration.Colors
            };
        }

        [UpdateOnInitialize]
        void CharacterCreatorPartSelectorData.IIndexListener.OnIndex(int index)
        {
            _bodyVisualsData.Type = _characterCreatorBodyConfiguration.Types[CharacterCreatorPartSelectorData.Index];
        }

        [UpdateOnInitialize]
        void PredefinedColorPickerData.IIndexListener.OnIndex(int index)
        {
            _bodyVisualsData.Color = _characterCreatorBodyConfiguration.Colors[index];
        }

        async void BodyVisualsData.ITypeListener.OnType(BodyType type)
        {
            var bodyAsset = CharacterVisualsAddressables.GetBodyVisualsConfiguration(type);
            var configuration = await bodyAsset.Task;

            _iconImage.sprite = configuration.Sprite;
        }

        void BodyVisualsData.IColorListener.OnColor(Color32 color)
        {
            _iconImage.color = color;
        }
    }
}