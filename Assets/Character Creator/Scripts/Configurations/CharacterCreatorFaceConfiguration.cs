using CityPop.Character;
using UnityEngine;

namespace CityPop.CharacterCreator.Configurations
{
    [CreateAssetMenu(menuName = "CityPop/Character Creator/Face", fileName = "Character Creator Face Configuration")]
    public class CharacterCreatorFaceConfiguration : ScriptableObject
    {
        public interface IAddedListener { void OnCharacterCreatorFaceConfiguration(CharacterCreatorFaceConfiguration data); }
        public interface IRemovedListener { void OnCharacterCreatorFaceConfigurationRemoved(); }
        
        [field: SerializeField]
        public FaceType[] Types { get; set; }
        [field: SerializeField]
        public Color32[] Colors { get; set; }
    }
}