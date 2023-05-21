using Zen.Core.Extensions;
using UnityEngine;
using Zen.Core.View;
using Zen.CodeGeneration.DataBinding.Attributes;

namespace CityPop.Character
{
    [DataBinding(typeof(BodyVisualsData))]

    public partial class BodyVisualsView : View
        , BodyVisualsData.ITypeListener
        , BodyVisualsData.IColorListener
    {
        [SerializeField] SpriteRenderer _bodyRenderer;

        [UpdateOnInitialize]
        void BodyVisualsData.ITypeListener.OnType(BodyType type)
        {
            var bodyAsset = CharacterVisualsAddressables.GetBodyVisualsConfiguration(type);
            var configuration = bodyAsset.WaitForCompletion();
            bodyAsset.Release();

            transform.localPosition = new Vector3(configuration.Position.x, configuration.Position.y, transform.localPosition.z);
            _bodyRenderer.sprite = configuration.Sprite;
        }

        [UpdateOnInitialize]
        void BodyVisualsData.IColorListener.OnColor(Color32 color)
        {
            _bodyRenderer.color = color;
        }
    }
}