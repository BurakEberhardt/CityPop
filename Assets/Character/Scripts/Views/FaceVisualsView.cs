using CityPop.Core;
using CityPop.Core.Shared.Attributes;
using Core.Extensions;
using UnityEngine;

namespace CityPop.Character
{
    [DataBinding(typeof(FaceVisualsData))]
    public partial class FaceVisualsView : View
        , FaceVisualsData.IAddedListener
        , FaceVisualsData.IRemovedListener
        , FaceVisualsData.ITypeListener
        , FaceVisualsData.IColorListener
    {
        [SerializeField] SpriteRenderer _faceRenderer;

        void FaceVisualsData.IAddedListener.OnAdded(FaceVisualsData faceVisualsData)
        {
        }

        void FaceVisualsData.IRemovedListener.OnRemoved()
        {
        }

        async void FaceVisualsData.ITypeListener.OnType(FaceType type)
        {
            var faceAsset = CharacterVisualsAddressables.GetFaceVisualsConfiguration(type);
            var configuration = await faceAsset.Task;
            faceAsset.Release();

            transform.localPosition = configuration.Position;
            _faceRenderer.sprite = configuration.Sprite;
        }

        void FaceVisualsData.IColorListener.OnColor(Color32 color)
        {
            _faceRenderer.color = color;
        }
    }
}