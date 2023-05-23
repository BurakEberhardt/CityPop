using UnityEngine;
using Zen.CodeGeneration.DataBinding.Attributes;

namespace Character.Scripts.Data
{
    [Data]
    public sealed partial class CharacterControllerData
    {
        public Vector2 StartPosition { get; set; }
    }
}