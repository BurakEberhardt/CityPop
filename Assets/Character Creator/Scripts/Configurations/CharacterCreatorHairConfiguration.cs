using CityPop.Character;
using UnityEngine;

namespace CityPop.CharacterCreator.Configurations
{
    [CreateAssetMenu(menuName = "CityPop/Character Creator/Hair", fileName = "Character Creator Hair Configuration")]
    public class CharacterCreatorHairConfiguration : ScriptableObject
    {
        public interface IAddedListener { void OnCharacterCreatorHairConfiguration(CharacterCreatorHairConfiguration data); }
        public interface IRemovedListener { void OnCharacterCreatorHairConfigurationRemoved(); }
        
        [field: SerializeField]
        public HairType[] Types { get; set; }
        [field: SerializeField]
        public Color32[] Colors { get; set; }
    }
}