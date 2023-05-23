using System;
using CityPop.Character;
using Newtonsoft.Json;
using Unity.Netcode;
using Zen.CodeGeneration.DataBinding.Attributes;

namespace CityPop.Player.Data
{
    [Data]
    public partial class PlayerData : INetworkSerializable
    {
        [JsonProperty] public Guid Guid { get; set; }
        [JsonProperty] public DateTime CreationDate { get; set; }
        [Data] CharacterData _character;
        
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _character);
        }
    }
}