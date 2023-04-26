using CityPop.Character;
using CityPop.Core.Shared.Attributes;
using UnityEngine;

namespace CityPop.CharacterCreator.Configurations
{
    [CreateAssetMenu(menuName = "CityPop/Character Creator/Body", fileName = "Character Creator Body Configuration")]
    [Data]
    public partial class CharacterCreatorBodyConfiguration : ScriptableObject
    {
        [field: SerializeField]
        public BodyType[] Types { get; set; }
        [field: SerializeField]
        public Color32[] Colors { get; set; }
    }
}