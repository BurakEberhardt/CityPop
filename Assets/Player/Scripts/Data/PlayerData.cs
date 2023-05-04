using CityPop.Character;
using Zen.CodeGeneration.DataBinding.Attributes;

namespace Player.Data
{
    [Data]
    public partial class PlayerData
    {
        [Data] CharacterData _character;
    }
}