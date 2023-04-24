using CityPop.Character;
using UnityEngine;

namespace CityPop.CharacterCreator.Configurations
{
    [CreateAssetMenu(menuName = "CityPop/Character Creator/Body", fileName = "Character Creator Body Configuration")]
    public class CharacterCreatorBodyConfiguration : ScriptableObject
    {
        public interface IAddedListener { void OnCharacterCreatorBodyConfiguration(CharacterCreatorBodyConfiguration data); }
        public interface IRemovedListener { void OnCharacterCreatorBodyConfigurationRemoved(); }
        
        [field: SerializeField]
        public BodyType[] Types { get; set; }
        [field: SerializeField]
        public Color32[] Colors { get; set; }
    }
}