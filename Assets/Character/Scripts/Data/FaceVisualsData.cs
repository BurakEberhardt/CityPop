using System;
using UnityEngine;
using Zen.CodeGeneration.DataBinding.Attributes;

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