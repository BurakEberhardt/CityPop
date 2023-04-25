using System;
using CityPop.Core.Shared.Attributes;
using UnityEngine;

namespace CityPop.Character
{
    public enum FaceType : byte
    {
    }

    
    [Serializable, Data]
    public partial class FaceVisualsData
    {
        [SerializeField, Data] FaceType _type;
        [SerializeField, Data] Color32 _color;
    }
}