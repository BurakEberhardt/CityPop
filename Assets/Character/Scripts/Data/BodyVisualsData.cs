using System;
using Unity.Netcode;
using UnityEngine;
using Zen.CodeGeneration.DataBinding.Attributes;

namespace CityPop.Character
{
    [Serializable, Data]
    public partial class BodyVisualsData : INetworkSerializable
    {
        [SerializeField, Data] BodyType _type;
        [SerializeField, Data] Color32 _color;
        
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _type);
            serializer.SerializeValue(ref _color);
        }
    }
}