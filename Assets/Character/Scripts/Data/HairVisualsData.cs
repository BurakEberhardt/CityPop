using System;
using Unity.Netcode;
using UnityEngine;
using Zen.CodeGeneration.DataBinding.Attributes;

namespace CityPop.Character
{
    [Serializable, Data]
    public partial class HairVisualsData : INetworkSerializable
    {
        [SerializeField, Data] HairType _type;
        [SerializeField, Data] Color32 _color;
        
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _type);
            serializer.SerializeValue(ref _color);
        }
    }
}