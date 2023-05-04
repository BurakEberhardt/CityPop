using System;
using UnityEngine;
using Zen.Shared.Attributes;

namespace CityPop.Character
{
    public enum HairType : byte
    {
    }
    
    [Serializable, Data]
    public partial class HairVisualsData
    {
        [SerializeField, Data] HairType _type;
        [SerializeField, Data] Color32 _color;
    }
}