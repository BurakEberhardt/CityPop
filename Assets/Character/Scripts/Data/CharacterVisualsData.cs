using System;
using CityPop.Core.Shared.Attributes;
using UnityEngine;

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