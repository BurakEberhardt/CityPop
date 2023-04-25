using System;
using CityPop.Character.Configurations;
using CityPop.Core;
using CityPop.Core.Shared.Attributes;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

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
        protected HairVisualsData _hairVisualsData;
        AsyncOperationHandle<HairConfiguration> _hairAsset;

        public HairVisualsData HairVisualsData
        {
            get => _hairVisualsData;
            set
            {
                if (_hairVisualsData != null)
                {
                    _hairVisualsData.RemoveTypeListener(this);
                    _hairVisualsData.RemoveColorListener(this);
                    (this as HairVisualsData.IRemovedListener).OnRemoved();
                }

                _hairVisualsData = value;

                if (_hairVisualsData != null)
                {
                    _hairVisualsData.AddTypeListener(this);
                    _hairVisualsData.AddColorListener(this);
                    (this as HairVisualsData.IAddedListener).OnAdded(_hairVisualsData);
                }
            }
        }

        void OnDestroy()
        {
            // Addressables.Release(_hairAsset);
        }

        void HairVisualsData.IAddedListener.OnAdded(HairVisualsData hairVisualsData)
        {
        }

        void HairVisualsData.IRemovedListener.OnRemoved()
        {
        }

        async void HairVisualsData.ITypeListener.OnType(HairType type)
        {
            // Addressables.Release(_hairAsset);
            _hairAsset = CharacterVisualsAddressables.GetHairVisualsConfiguration(type);
            var configuration = await _hairAsset.Task;

            transform.localPosition = configuration.Position;
            _hairRenderer.sprite = configuration.Sprite;
        }

        void HairVisualsData.IColorListener.OnColor(Color32 color)
        {
            _hairRenderer.color = color;
        }
    }
}