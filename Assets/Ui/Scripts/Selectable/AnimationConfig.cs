using UnityEngine;

namespace Zen.Ui.Selectable
{
    [CreateAssetMenu(menuName = "Zen/Ui/Selectable/Animation", fileName = "Animation")]
    public class AnimationConfig : ScriptableObject
    {
        public float TransitionDuration;
        public string Normal = "Normal";
        public string Highlighted = "Highlighted";
        public string Pressed = "Pressed";
        public string Selected = "Selected";
        public string Disabled = "Disabled";
    }
}