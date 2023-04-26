using CityPop.Character;
using CityPop.Core.Shared.Attributes;
using UnityEngine;

namespace CityPop.CharacterCreator.Configurations
{
    [CreateAssetMenu(menuName = "CityPop/Character Creator/Hair", fileName = "Character Creator Hair Configuration")]
    [Data]
    public partial class CharacterCreatorHairConfiguration : ScriptableObject
    {
        [field: SerializeField]
        public HairType[] Types { get; set; }
        [field: SerializeField]
        public Color32[] Colors { get; set; }
    }
}