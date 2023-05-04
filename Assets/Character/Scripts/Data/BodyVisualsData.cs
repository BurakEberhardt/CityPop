using System;
using UnityEngine;
using Zen.CodeGeneration.DataBinding.Attributes;

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