using System;
using UnityEngine;

namespace Zen.Ui.Selectable
{
    [CreateAssetMenu(menuName = "Zen/Ui/Selectable/Emission", fileName = "Emission")]
    public class EmissionConfig : ScriptableObject
    {
        public Config Normal;
        public Config Highlighted;
        public Config Pressed;
        public Config Selected;
        public Config Disabled;
        public float FadeDuration;

        [Serializable]
        public class Config
        {
            public float Strength;
            public Color Color = UnityEngine.Color.white;
        }
    }
}