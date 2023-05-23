using System;
using Unity.Netcode;
using Zen.CodeGeneration.DataBinding.Attributes;

namespace CityPop.World.Data
{
    [Serializable]
    public partial class WorldData : INetworkSerializable
    {
        [Data] WorldPosition _spawnPosition;
        
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _spawnPosition);
        }
    }
}