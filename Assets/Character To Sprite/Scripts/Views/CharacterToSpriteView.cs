using System.Collections.Generic;
using System.Diagnostics;
using CityPop.Character;
using CityPop.CharacterToTexture.Data;
using CityPop.Core.ListSynchronizer;
using UnityEngine;
using Zen.Core.View;
using Zen.CodeGeneration.DataBinding.Attributes;
using Debug = UnityEngine.Debug;

namespace CityPop.CharacterToTexture.Views
{
    [DataBinding(typeof(CharacterToSpriteData))]
    public partial class CharacterToSpriteView : MonoBehaviour
        , CharacterToSpriteData.ICharacterSpritesListener
    {
        [SerializeField] Camera _camera;
        [SerializeField] Transform _cameraTransform;
        [SerializeField] CharacterVisualsView _characterVisualsView;
        [SerializeField] Vector2Int _spriteSize;

        RenderTexture _renderTexture;
        int _rows;
        int _columns;
        int Capacity => _rows * _columns;

        readonly List<CharacterVisualsView> _characterVisualsViews = new();
        ListSynchronizer<CharacterVisualsData, CharacterVisualsView, CharacterSpriteData> _characterSpritesViewDataSynchronizer;

        public void OnCharacterSprites(List<CharacterSpriteData> characterSprites)
        {
            Debug.Log($"{nameof(OnCharacterSprites)}({characterSprites.Count})");

            var stopwatch = Stopwatch.StartNew();

            if (Capacity < characterSprites.Count || Capacity > characterSprites.Count * 2)
                CreateRTWithCapacity(characterSprites.Count);

            _characterSpritesViewDataSynchronizer ??= new ListSynchronizer<CharacterVisualsData, CharacterVisualsView, CharacterSpriteData>(
                view => view.CharacterVisualsData,
                data => data.Character.Visuals,
                CreateView,
                DeleteView,
                UpdateView);
            
            _characterSpritesViewDataSynchronizer.Synchronize(_characterVisualsViews, characterSprites);

            gameObject.SetActive(characterSprites.Count > 0);

            stopwatch.Stop();

            Debug.Log($"Elapsed: {stopwatch.ElapsedTicks}");
        }

        CharacterVisualsView CreateView(CharacterSpriteData data, int index)
        {
            var characterVisualsView = _characterVisualsView.GetViewFromObjectPool(transform);
            characterVisualsView.CharacterVisualsData = data.Character.Visuals;
            UpdateView(characterVisualsView, data, -1, index);

            return characterVisualsView;
        }

        static void DeleteView(CharacterVisualsView view, int index)
        {
            view.CharacterVisualsData = null;
            view.PushViewToObjectPool();
        }

        void UpdateView(CharacterVisualsView view, CharacterSpriteData data, int fromIndex, int toIndex)
        {
            var x = toIndex % _columns;
            var y = toIndex / _columns;

            data.Sprite = new CharacterSpriteData.RTSprite()
            {
                Texture = _renderTexture,
                Rect = new Rect(x * _spriteSize.x, y * _spriteSize.y, _spriteSize.x, _spriteSize.y)
            };

            view.transform.localPosition = new Vector3(x + 0.5f, y + 0.5f);
        }

        void CreateRTWithCapacity(int capacity)
        {
            Debug.Log($"{nameof(CreateRTWithCapacity)}({capacity})");
            _rows = _columns = 1;

            var textureSize = _spriteSize;

            while (_rows * _columns < capacity)
            {
                if (textureSize.x > textureSize.y)
                {
                    ++_rows;
                    textureSize.y += _spriteSize.y;
                }
                else
                {
                    ++_columns;
                    textureSize.x += _spriteSize.x;
                }
            }

            if (_renderTexture)
            {
                _renderTexture.Release();
                Destroy(_renderTexture);
            }

            _renderTexture = new RenderTexture(textureSize.x, textureSize.y, 1);
            _camera.targetTexture = _renderTexture;
            _camera.orthographicSize = _rows * 0.5f;
            _cameraTransform.localPosition = new Vector3(_columns * 0.5f, _rows * 0.5f, -1f);
        }
    }
}