using System;
using CityPop.Character.Configurations;
using CityPop.Core;
using CityPop.Core.Attributes;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CityPop.Character
{
    [ViewData(typeof(HairVisualsData))]
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
                    OnHairVisualsDataRemoved();
                }

                _hairVisualsData = value;

                if (_hairVisualsData != null)
                {
                    _hairVisualsData.AddTypeListener(this);
                    _hairVisualsData.AddColorListener(this);
                    OnHairVisualsData(_hairVisualsData);
                }
            }
        }

        void OnDestroy()
        {
            // Addressables.Release(_hairAsset);
        }

        public void OnHairVisualsData(HairVisualsData data)
        {
        }

        public void OnHairVisualsDataRemoved()
        {
        }

        public async void OnHairType(HairType type)
        {
            // Addressables.Release(_hairAsset);
            _hairAsset = CharacterVisualsAddressables.GetHairVisualsConfiguration(type);
            var configuration = await _hairAsset.Task;

            transform.localPosition = configuration.Position;
            _hairRenderer.sprite = configuration.Sprite;
        }


        public void OnHairColor(Color32 color)
        {
            _hairRenderer.color = color;
        }
    }
}