using UnityEngine;

namespace Zen.Ui.Selectable
{
    [CreateAssetMenu(menuName = "Zen/Ui/Selectable/Sprite Swap", fileName = "Sprite Swap")]
    public class SpriteSwapConfig : ScriptableObject
    {
        public Sprite Normal;
        public Sprite Highlighted;
        public Sprite Pressed;
        public Sprite Selected;
        public Sprite Disabled;
    }
}