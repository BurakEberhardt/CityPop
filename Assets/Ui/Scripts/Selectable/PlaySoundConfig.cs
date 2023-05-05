using UnityEngine;

namespace Zen.Ui.Selectable
{
    [CreateAssetMenu(menuName = "Zen/Ui/Selectable/Play Sound", fileName = "Play Sound")]
    public class PlaySoundConfig : ScriptableObject
    {
        public AudioClip Normal;
        public AudioClip Highlighted;
        public AudioClip Pressed;
        public AudioClip Selected;
        public AudioClip Disabled;
    }
}