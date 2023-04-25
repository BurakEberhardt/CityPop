using System;
using CityPop.Core.Shared.Attributes;
using UnityEngine;

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