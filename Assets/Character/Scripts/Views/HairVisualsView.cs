using CityPop.Core;
using CityPop.Core.Shared.Attributes;
using Core.Extensions;
using UnityEngine;
using Zen.Core.View;

namespace CityPop.Character
{
    [DataBinding(typeof(HairVisualsData))]
    public partial class HairVisualsView : View
        , HairVisualsData.ITypeListener
        , HairVisualsData.IColorListener
    {
        [SerializeField] SpriteRenderer _hairRenderer;

        [UpdateOnInitialize]
        void HairVisualsData.ITypeListener.OnType(HairType type)
        {
            var hairAsset = CharacterVisualsAddressables.GetHairVisualsConfiguration(type);
            var configuration = hairAsset.WaitForCompletion();
            hairAsset.Release();

            transform.localPosition = configuration.Position;
            _hairRenderer.sprite = configuration.Sprite;
        }

        [UpdateOnInitialize]
        void HairVisualsData.IColorListener.OnColor(Color32 color)
        {
            _hairRenderer.color = color;
        }
    }
}