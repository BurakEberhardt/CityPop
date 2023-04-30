using CityPop.Character;
using CityPop.Core.Shared.Attributes;

namespace Player.Data
{
    [Data]
    public partial class PlayerData
    {
        [Data] CharacterData _character;
    }
}