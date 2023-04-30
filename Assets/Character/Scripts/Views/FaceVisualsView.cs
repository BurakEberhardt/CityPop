using CityPop.Core;
using CityPop.Core.Shared.Attributes;
using Core.Extensions;
using UnityEngine;

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

            transform.localPosition = configuration.Position;
            _faceRenderer.sprite = configuration.Sprite;
        }

        [UpdateOnInitialize]
        void FaceVisualsData.IColorListener.OnColor(Color32 color)
        {
            _faceRenderer.color = color;
        }
    }
}