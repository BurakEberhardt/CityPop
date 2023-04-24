using System;
using CityPop.Character.Configurations;
using CityPop.Core;
using CityPop.Core.Shared.Attributes;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

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
        protected BodyVisualsData _bodyVisualsData;
        AsyncOperationHandle<BodyConfiguration> _bodyAsset;

        public BodyVisualsData BodyVisualsData
        {
            get => _bodyVisualsData;
            set
            {
                if (_bodyVisualsData != null)
                {
                    _bodyVisualsData.RemoveTypeListener(this);
                    _bodyVisualsData.RemoveColorListener(this);
                    OnBodyVisualsDataRemoved();
                }

                _bodyVisualsData = value;

                if (_bodyVisualsData != null)
                {
                    _bodyVisualsData.AddTypeListener(this);
                    _bodyVisualsData.AddColorListener(this);
                    OnBodyVisualsData(_bodyVisualsData);
                }
            }
        }

        void OnDestroy()
        {
            // Addressables.Release(_bodyAsset);
        }

        public void OnBodyVisualsData(BodyVisualsData data)
        {
        }

        public void OnBodyVisualsDataRemoved()
        {
        }

        public async void OnBodyType(BodyType type)
        {
            // Addressables.Release(_bodyAsset);
            _bodyAsset = CharacterVisualsAddressables.GetBodyVisualsConfiguration(type);
            var configuration = await _bodyAsset.Task;

            transform.localPosition = configuration.Position;
            _bodyRenderer.sprite = configuration.Sprite;
        }


        public void OnBodyColor(Color32 color)
        {
            _bodyRenderer.color = color;
        }
    }
}