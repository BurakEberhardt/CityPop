using System;
using CityPop.Character.Configurations;
using CityPop.Core;
using CityPop.Core.Attributes;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CityPop.Character
{
    [ViewData(typeof(FaceVisualsData))]
    public partial class FaceVisualsView : View
        , FaceVisualsData.IAddedListener
        , FaceVisualsData.IRemovedListener
        , FaceVisualsData.ITypeListener
        , FaceVisualsData.IColorListener
    {
        [SerializeField] SpriteRenderer _faceRenderer;
        protected FaceVisualsData _faceVisualsData;
        AsyncOperationHandle<FaceConfiguration> _faceAsset;

        public FaceVisualsData FaceVisualsData
        {
            get => _faceVisualsData;
            set
            {
                if (_faceVisualsData != null)
                {
                    _faceVisualsData.RemoveTypeListener(this);
                    _faceVisualsData.RemoveColorListener(this);
                    OnFaceVisualsDataRemoved();
                }

                _faceVisualsData = value;

                if (_faceVisualsData != null)
                {
                    _faceVisualsData.AddTypeListener(this);
                    _faceVisualsData.AddColorListener(this);
                    OnFaceVisualsData(_faceVisualsData);
                }
            }
        }

        void OnDestroy()
        {
            // Addressables.Release(_faceAsset);
        }

        public void OnFaceVisualsData(FaceVisualsData data)
        {
        }

        public void OnFaceVisualsDataRemoved()
        {
        }

        public async void OnFaceType(FaceType type)
        {
            // Addressables.Release(_faceAsset);
            _faceAsset = CharacterVisualsAddressables.GetFaceVisualsConfiguration(type);
            var configuration = await _faceAsset.Task;

            transform.localPosition = configuration.Position;
            _faceRenderer.sprite = configuration.Sprite;
        }


        public void OnFaceColor(Color32 color)
        {
            _faceRenderer.color = color;
        }
    }
}