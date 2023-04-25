using CityPop.Character.Configurations;
using CityPop.Core;
using CityPop.Core.Shared.Attributes;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

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
        AsyncOperationHandle<FaceConfiguration> _faceAsset;

        protected FaceVisualsData _faceVisualsData;
        public FaceVisualsData FaceVisualsData
        {
            get => _faceVisualsData;
            set
            {
                if (_faceVisualsData != null)
                {
                    _faceVisualsData.RemoveTypeListener(this);
                    _faceVisualsData.RemoveColorListener(this);
                    (this as FaceVisualsData.IRemovedListener).OnRemoved();
                }

                _faceVisualsData = value;

                if (_faceVisualsData != null)
                {
                    _faceVisualsData.AddTypeListener(this);
                    _faceVisualsData.AddColorListener(this);
                    (this as FaceVisualsData.IAddedListener).OnAdded(_faceVisualsData);
                }
            }
        }

        void OnDestroy()
        {
            // Addressables.Release(_faceAsset);
        }

        void FaceVisualsData.IAddedListener.OnAdded(FaceVisualsData faceVisualsData)
        {
        }

        void FaceVisualsData.IRemovedListener.OnRemoved()
        {
        }

        async void FaceVisualsData.ITypeListener.OnType(FaceType type)
        {
            // Addressables.Release(_faceAsset);
            _faceAsset = CharacterVisualsAddressables.GetFaceVisualsConfiguration(type);
            var configuration = await _faceAsset.Task;

            transform.localPosition = configuration.Position;
            _faceRenderer.sprite = configuration.Sprite;
        }

        void FaceVisualsData.IColorListener.OnColor(Color32 color)
        {
            _faceRenderer.color = color;
        }
    }
}