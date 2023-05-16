using System;
using CityPop.Character;
using Newtonsoft.Json;
using Zen.CodeGeneration.DataBinding.Attributes;

namespace Player.Data
{
    [Data]
    public partial class PlayerData
    {
        [JsonProperty] public Guid Guid { get; set; }
        [JsonProperty] public DateTime CreationDate { get; set; }
        [Data] CharacterData _character;
    }
}