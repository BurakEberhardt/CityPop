using System;
using System.Collections.Generic;
using CityPop.Core.Shared.Attributes;
using UnityEngine;

namespace CityPop.Character
{
    [Data]
    public partial class TestData
    {
        [SerializeField, Data] int _test;
        [SerializeField, Data] Color32 _color;
        [SerializeField, Data] string _name;
    }
}