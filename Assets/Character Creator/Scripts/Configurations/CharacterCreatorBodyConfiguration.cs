using CityPop.Character;
using UnityEngine;
using Zen.CodeGeneration.DataBinding.Attributes;

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