using Unity.Netcode;
using Zen.CodeGeneration.DataBinding.Attributes;

namespace CityPop.Character
{
    [Data]
    public partial class CharacterData : INetworkSerializable
    {
        [Data] string _name;
        [Data] CharacterVisualsData _visuals;
        
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _name);
            serializer.SerializeValue(ref _visuals);
        }
    }
}