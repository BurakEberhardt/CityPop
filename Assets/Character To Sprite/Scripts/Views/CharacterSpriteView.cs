using CityPop.Character;
using CityPop.CharacterToTexture.Data;
using Core;
using UnityEngine;
using UnityEngine.UI;
using Zen.Core.View;
using Zen.CodeGeneration.DataBinding.Attributes;
using Zen.CodeGeneration.UnityMethods.Attributes;

namespace CityPop.CharacterToTexture.Views
{
    [DataBinding(typeof(CharacterSpriteData), Accessibility.Private)]
    [DataBinding(typeof(CharacterData))]
    public partial class CharacterSpriteView : View
        , CharacterSpriteData.ISpriteListener
        , CharacterSpriteData.IRemovedListener
        , CharacterData.IAddedListener
        , CharacterData.IRemovedListener
    {
        [SerializeField] RawImage _image;
        Texture2D _texture;

        [Awake]
        void CreateTexture()
        {
            _texture = new Texture2D(256, 256)
            {
                filterMode = FilterMode.Bilinear
            };
            
            _image.texture = _texture;
        }

        [OnDestroy]
        void DestroyTexture()
        {
            Destroy(_texture);
        }

        void CharacterData.IAddedListener.OnAdded(CharacterData characterData)
        {
            CharacterSpriteData = new CharacterSpriteData() {Character = characterData};
            var characterToSpriteData = ServiceLocator.Get<CharacterToSpriteService>().Data;
            characterToSpriteData.CharacterSprites.Add(CharacterSpriteData);
            // TODO: Introduce add for IList data types so I don't have to trigger the event like this
            characterToSpriteData.CharacterSprites = characterToSpriteData.CharacterSprites;
        }

        void CharacterData.IRemovedListener.OnRemoved()
        {
            var characterToSpriteData = ServiceLocator.Get<CharacterToSpriteService>().Data;
            characterToSpriteData.CharacterSprites.Remove(CharacterSpriteData);
            // TODO: Introduce remove listeners for IList data types so I don't have to trigger the event like this
            characterToSpriteData.CharacterSprites = characterToSpriteData.CharacterSprites;
            CharacterSpriteData = null;
        }

        void CharacterSpriteData.IRemovedListener.OnRemoved()
        {
            _image.texture = Texture2D.whiteTexture;
        }

        void CharacterSpriteData.ISpriteListener.OnSprite(CharacterSpriteData.RTSprite sprite)
        {
            _image.texture = sprite.Texture;
            _image.uvRect = new Rect(sprite.Rect.x / (float)sprite.Texture.width, sprite.Rect.y / (float)sprite.Texture.height, sprite.Rect.width / (float)sprite.Texture.width, sprite.Rect.height / (float)sprite.Texture.height);
        }
    }
}