using CityPop.Character;
using CityPop.Core.Shared.Attributes;
using UnityEngine;

namespace CharacterToTexture.Data
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