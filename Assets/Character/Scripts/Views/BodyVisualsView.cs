using CityPop.Character.Configurations;
using CityPop.Core;
using CityPop.Core.Shared.Attributes;
using UnityEngine;
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
        AsyncOperationHandle<BodyConfiguration> _bodyAsset;

        protected BodyVisualsData _bodyVisualsData;
        public BodyVisualsData BodyVisualsData
        {
            get => _bodyVisualsData;
            set
            {
                if (_bodyVisualsData != null)
                {
                    _bodyVisualsData.RemoveTypeListener(this);
                    _bodyVisualsData.RemoveColorListener(this);
                    (this as BodyVisualsData.IRemovedListener).OnRemoved();
                }

                _bodyVisualsData = value;

                if (_bodyVisualsData != null)
                {
                    _bodyVisualsData.AddTypeListener(this);
                    _bodyVisualsData.AddColorListener(this);
                    (this as BodyVisualsData.IAddedListener).OnAdded(_bodyVisualsData);
                }
            }
        }

        void OnDestroy()
        {
            // Addressables.Release(_bodyAsset);
        }

        void BodyVisualsData.IAddedListener.OnAdded(BodyVisualsData bodyVisualsData)
        {
        }

        void BodyVisualsData.IRemovedListener.OnRemoved()
        {
        }

        async void BodyVisualsData.ITypeListener.OnType(BodyType type)
        {
            // Addressables.Release(_bodyAsset);
            _bodyAsset = CharacterVisualsAddressables.GetBodyVisualsConfiguration(type);
            var configuration = await _bodyAsset.Task;

            transform.localPosition = configuration.Position;
            _bodyRenderer.sprite = configuration.Sprite;
        }

        void BodyVisualsData.IColorListener.OnColor(Color32 color)
        {
            _bodyRenderer.color = color;
        }
    }
}