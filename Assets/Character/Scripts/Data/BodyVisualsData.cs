using System;
using CityPop.Core.Shared.Attributes;
using UnityEngine;

namespace CityPop.Character
{
    public enum BodyType : byte
    {
    }

    [Serializable, Data]
    public partial class BodyVisualsData
    {
        [SerializeField, Data] BodyType _type;
        [SerializeField, Data] Color32 _color;
    }
}