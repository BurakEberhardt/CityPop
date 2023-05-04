using CityPop.Character;
using UnityEngine;
using Zen.Shared.Attributes;

namespace CityPop.CharacterToTexture.Data
{
    [Data]
    public partial class CharacterSpriteData
    {
        public CharacterData Character { get; set; }
        [Data] RTSprite _sprite;

        public struct RTSprite
        {
            public RenderTexture Texture;
            public Rect Rect;
        }
    }
}