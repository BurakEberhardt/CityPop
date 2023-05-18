using System.Collections.Generic;
using CityPop.Player.Data;
using Zen.CodeGeneration.DataBinding.Attributes;

namespace SavegameSelector.Data
{
    [Data]
    public partial class SavegameSelectorData
    {
        [Data] List<PlayerData> _players;
    }
}