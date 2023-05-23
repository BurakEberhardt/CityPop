using System;
using Unity.Netcode;
using UnityEngine;
using Zen.CodeGeneration.DataBinding.Attributes;

namespace CityPop.World.Data
{
    [Serializable]
    public partial class WorldPosition : INetworkSerializable
    {
        [Data] Vector2 _position;
        [Data] MapId _mapId;
        
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _position);
            serializer.SerializeValue(ref _mapId);
        }
    }
}