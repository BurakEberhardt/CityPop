using UnityEngine;

namespace CityPop.Character.Configurations
{
    [CreateAssetMenu(menuName = "CityPop/Character/Face", fileName = "Face")]
    public class FaceConfiguration : ScriptableObject
    {
        [field: SerializeField] public Sprite Sprite  { get; set; }
        [field: SerializeField] public Vector2 Position  { get; set; }
    }
}