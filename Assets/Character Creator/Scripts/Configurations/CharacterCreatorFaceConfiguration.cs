using CityPop.Character;
using UnityEngine;
using Zen.Shared.Attributes;

namespace CityPop.CharacterCreator.Configurations
{
    [CreateAssetMenu(menuName = "CityPop/Character Creator/Face", fileName = "Character Creator Face Configuration")]
    [Data]
    public partial class CharacterCreatorFaceConfiguration : ScriptableObject
    {
        [field: SerializeField]
        public FaceType[] Types { get; set; }
        [field: SerializeField]
        public Color32[] Colors { get; set; }
    }
}