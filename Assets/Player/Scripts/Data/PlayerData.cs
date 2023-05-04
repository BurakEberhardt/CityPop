using CityPop.Character;
using Zen.Shared.Attributes;

namespace Player.Data
{
    [Data]
    public partial class PlayerData
    {
        [Data] CharacterData _character;
    }
}