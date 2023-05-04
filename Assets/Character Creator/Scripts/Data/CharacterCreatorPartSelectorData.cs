using Zen.CodeGeneration.DataBinding.Attributes;

namespace CharacterCreator.Data
{
    [Data]
    public partial class CharacterCreatorPartSelectorData
    {
        [Data] int _index;
        public int Count { get; set; }
    }
}