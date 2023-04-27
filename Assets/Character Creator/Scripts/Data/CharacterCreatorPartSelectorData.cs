using CityPop.Core.Shared.Attributes;

namespace CharacterCreator.Data
{
    [Data]
    public partial class CharacterCreatorPartSelectorData
    {
        [Data] int _index;
        public int Count { get; set; }
    }
}