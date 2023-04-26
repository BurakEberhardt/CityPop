using CityPop.Core;
using CityPop.Core.Shared.Attributes;
using Core.Extensions;
using UnityEngine;

namespace CityPop.Character
{
    [DataBinding(typeof(HairVisualsData))]
    public partial class HairVisualsView : View
        , HairVisualsData.IAddedListener
        , HairVisualsData.IRemovedListener
        , HairVisualsData.ITypeListener
        , HairVisualsData.IColorListener
    {
        [SerializeField] SpriteRenderer _hairRenderer;
        
        void HairVisualsData.IAddedListener.OnAdded(HairVisualsData hairVisualsData)
        {
        }

        void HairVisualsData.IRemovedListener.OnRemoved()
        {
        }

        async void HairVisualsData.ITypeListener.OnType(HairType type)
        {
            var hairAsset = CharacterVisualsAddressables.GetHairVisualsConfiguration(type);
            var configuration = await hairAsset.Task;
            hairAsset.Release();

            transform.localPosition = configuration.Position;
            _hairRenderer.sprite = configuration.Sprite;
        }

        void HairVisualsData.IColorListener.OnColor(Color32 color)
        {
            _hairRenderer.color = color;
        }
    }
}