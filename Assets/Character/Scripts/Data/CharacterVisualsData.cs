using System;
using Unity.Netcode;
using UnityEngine;
using Zen.CodeGeneration.DataBinding.Attributes;

namespace CityPop.Character
{
    [Serializable, Data]
    public partial class CharacterVisualsData : INetworkSerializable
    {
        [SerializeField, Data] BodyVisualsData _body = new();
        [SerializeField, Data] HairVisualsData _hair = new();
        [SerializeField, Data] FaceVisualsData _face = new();
        
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _body);
            serializer.SerializeValue(ref _hair);
            serializer.SerializeValue(ref _face);
        }
    }
}