using UnityEngine;

namespace CityPop.Character.Configurations
{
    [CreateAssetMenu(menuName = "CityPop/Character/Body", fileName = "Body")]
    public class BodyConfiguration : ScriptableObject
    {
        [field: SerializeField] public Sprite Sprite { get; set; }
        [field: SerializeField] public Vector2 Position { get; set; }
    }
}