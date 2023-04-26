using CityPop.Core;
using CityPop.Core.Shared.Attributes;
using Core.Extensions;
using UnityEngine;

namespace CityPop.Character
{
    [DataBinding(typeof(BodyVisualsData))]

    public partial class BodyVisualsView : View
        , BodyVisualsData.IAddedListener
        , BodyVisualsData.IRemovedListener
        , BodyVisualsData.ITypeListener
        , BodyVisualsData.IColorListener
    {
        [SerializeField] SpriteRenderer _bodyRenderer;

        void BodyVisualsData.IAddedListener.OnAdded(BodyVisualsData bodyVisualsData)
        {
        }

        void BodyVisualsData.IRemovedListener.OnRemoved()
        {
        }

        async void BodyVisualsData.ITypeListener.OnType(BodyType type)
        {
            var bodyAsset = CharacterVisualsAddressables.GetBodyVisualsConfiguration(type);
            var configuration = await bodyAsset.Task;
            bodyAsset.Release();

            transform.localPosition = configuration.Position;
            _bodyRenderer.sprite = configuration.Sprite;
        }

        void BodyVisualsData.IColorListener.OnColor(Color32 color)
        {
            _bodyRenderer.color = color;
        }
    }
}