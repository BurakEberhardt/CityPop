using CityPop.Character;
using UnityEngine;
using Zen.CodeGeneration.DataBinding.Attributes;

namespace CityPop.CharacterToTexture.Data
{
    [Data]
    public partial class CharacterSpriteData
    {
        public CharacterVisualsData CharacterVisuals { get; set; }
        [Data] RTSprite _sprite;

        public struct RTSprite
        {
            public RenderTexture Texture;
            public RectInt Rect;
        }
    }
}