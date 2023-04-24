using UnityEngine;

namespace CityPop.Character.Configurations
{
    [CreateAssetMenu(menuName = "CityPop/Character/Hair", fileName = "Hair")]
    public class HairConfiguration : ScriptableObject
    {
        [field: SerializeField] public Sprite Sprite  { get; set; }
        [field: SerializeField] public Vector2 Position  { get; set; }
    }
}