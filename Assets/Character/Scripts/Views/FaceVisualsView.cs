﻿using Zen.Core.Extensions;
using UnityEngine;
using Zen.Core.View;
using Zen.CodeGeneration.DataBinding.Attributes;

namespace CityPop.Character
{
    [DataBinding(typeof(FaceVisualsData))]
    public partial class FaceVisualsView : View
        , FaceVisualsData.ITypeListener
        , FaceVisualsData.IColorListener
    {
        [SerializeField] SpriteRenderer _faceRenderer;

        [UpdateOnInitialize]
        void FaceVisualsData.ITypeListener.OnType(FaceType type)
        {
            var faceAsset = CharacterVisualsAddressables.GetFaceVisualsConfiguration(type);
            var configuration = faceAsset.WaitForCompletion();
            faceAsset.Release();

            transform.localPosition = new Vector3(configuration.Position.x, configuration.Position.y, transform.localPosition.z);
            _faceRenderer.sprite = configuration.Sprite;
        }

        [UpdateOnInitialize]
        void FaceVisualsData.IColorListener.OnColor(Color32 color)
        {
            _faceRenderer.color = color;
        }
    }
}