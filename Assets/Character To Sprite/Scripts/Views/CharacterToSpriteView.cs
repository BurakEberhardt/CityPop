using System;
using System.Collections.Generic;
using CharacterToTexture.Data;
using CityPop.Character;
using CityPop.Core.Shared.Attributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace CharacterToTexture.Views
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

        List<CharacterVisualsView> _characterVisualsViews = new();

        public void OnCharacterSprites(List<CharacterSpriteData> characterSprites)
        {
            if (Capacity < characterSprites.Count || Capacity > characterSprites.Count * 2)
                CreateRTWithCapacity(characterSprites.Count);

            foreach (var characterVisualsView in _characterVisualsViews)
                Destroy(characterVisualsView.gameObject);

            _characterVisualsViews.Clear();
            _characterVisualsViews.Capacity = Math.Max(_characterVisualsViews.Capacity, characterSprites.Count);

            for (var i = 0; i < characterSprites.Count; ++i)
            {
                var characterSpriteData = characterSprites[i];

                var x = i % _columns;
                var y = i / _columns;

                characterSpriteData.Sprite = new CharacterSpriteData.RTSprite()
                {
                    Texture = _renderTexture,
                    Rect = new Rect(x * _spriteSize.x, y * _spriteSize.y, _spriteSize.x, _spriteSize.y)
                };

                var characterVisualsView = Instantiate(_characterVisualsView, transform);
                characterVisualsView.CharacterVisualsData = characterSpriteData.Character.Visuals;
                characterVisualsView.transform.localPosition = new Vector3(x + 0.5f, y + 0.5f);
                _characterVisualsViews.Add(characterVisualsView);
            }

            gameObject.SetActive(characterSprites.Count > 0);
        }

        void CreateRTWithCapacity(int capacity)
        {
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