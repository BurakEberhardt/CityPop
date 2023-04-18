using UnityEngine;

namespace Zen.Ui.Selectable
{
    [CreateAssetMenu(menuName = "Zen/Ui/Selectable/Color Tween", fileName = "Color Tween")]
    public class ColorTweenConfig : ScriptableObject
    {
        public float TransitionDuration;
        public Color Normal;
        public Color Highlighted;
        public Color Pressed;
        public Color Selected;
        public Color Disabled;
    }
}