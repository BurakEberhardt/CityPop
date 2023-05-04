﻿using Zen.CodeGeneration.DataBinding.Attributes;

namespace CityPop.Character
{
    [Data]
    public partial class CharacterData
    {
        [Data] string _name;
        [Data] CharacterVisualsData _visuals;
    }
}