using System.Collections.Generic;
using Player.Data;
using Zen.Shared.Attributes;

namespace SavegameSelector.Data
{
    [Data]
    public partial class SavegameSelectorData
    {
        [Data] List<PlayerData> _players;
    }
}