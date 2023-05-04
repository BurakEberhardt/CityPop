using CityPop.Character;
using CityPop.CharacterToTexture.Data;
using Core;
using UnityEngine;
using UnityEngine.UI;
using Zen.Core.View;
using Zen.Shared.Attributes;

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

        void CharacterData.IAddedListener.OnAdded(CharacterData characterData)
        {
            CharacterSpriteData = new CharacterSpriteData() { Character = characterData };
            var characterToSpriteData = ServiceLocator.GetOrCreate<CharacterToSpriteService>().Data;
            characterToSpriteData.CharacterSprites.Add(CharacterSpriteData);
            // TODO: Introduce add for IList data types so I don't have to trigger the event like this
            characterToSpriteData.CharacterSprites = characterToSpriteData.CharacterSprites; 
        }

        void CharacterData.IRemovedListener.OnRemoved()
        {
            var characterToSpriteData = ServiceLocator.GetOrCreate<CharacterToSpriteService>().Data;
            characterToSpriteData.CharacterSprites.Remove(CharacterSpriteData);
            // TODO: Introduce remove listeners for IList data types so I don't have to trigger the event like this
            characterToSpriteData.CharacterSprites = characterToSpriteData.CharacterSprites;
            CharacterSpriteData = null;
        }

        void CharacterSpriteData.IRemovedListener.OnRemoved()
        {
            _image.texture = null;
        }

        void CharacterSpriteData.ISpriteListener.OnSprite(CharacterSpriteData.RTSprite sprite)
        {
            _image.texture = sprite.Texture;
            _image.uvRect = new Rect(sprite.Rect.x / sprite.Texture.width, sprite.Rect.y / sprite.Texture.height, sprite.Rect.width / sprite.Texture.width, sprite.Rect.height / sprite.Texture.height);
        }
    }
}