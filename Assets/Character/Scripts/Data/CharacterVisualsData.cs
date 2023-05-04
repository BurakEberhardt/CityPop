using System;
using UnityEngine;
using Zen.CodeGeneration.DataBinding.Attributes;

namespace CityPop.Character
{
    [Serializable, Data]
    public partial class CharacterVisualsData
    {
        [SerializeField, Data] BodyVisualsData _body = new();
        [SerializeField, Data] HairVisualsData _hair = new();
        [SerializeField, Data] FaceVisualsData _face = new();
    }
}