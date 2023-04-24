using UnityEngine;

namespace CityPop.CharacterCreator.Configurations
{
    [CreateAssetMenu(menuName = "CityPop/Character Creator/Main Configuration", fileName = "Character Creator Configuration")]
    public class CharacterCreatorConfiguration : ScriptableObject
    {
        [field: SerializeField] public CharacterCreatorBodyConfiguration Body  { get; set; }
        [field: SerializeField] public CharacterCreatorHairConfiguration Hair  { get; set; }
        [field: SerializeField] public CharacterCreatorFaceConfiguration Face  { get; set; }
    }
}