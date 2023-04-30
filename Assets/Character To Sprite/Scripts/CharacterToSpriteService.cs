using System.Collections.Generic;
using CharacterToTexture.Data;
using CharacterToTexture.Views;
using Core.Extensions;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CharacterToTexture.Scripts
{
    public sealed class CharacterToSpriteService
    {
        public CharacterToSpriteData Data { get; }
        CharacterToSpriteView _view;

        public CharacterToSpriteService()
        {
            Data = new CharacterToSpriteData() {CharacterSprites = new List<CharacterSpriteData>()};

            var operationHandle = Addressables.LoadAssetAsync<GameObject>(nameof(CharacterToSpriteService));
            var prefab = operationHandle.WaitForCompletion();
            var gameObject = Object.Instantiate(prefab);
            gameObject.DontDestroyOnLoad();
            _view = gameObject.GetComponent<CharacterToSpriteView>();
            operationHandle.Release();

            _view.CharacterToSpriteData = Data;
        }
    }
}