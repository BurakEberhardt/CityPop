﻿using System.Collections.Generic;
using CityPop.Core.Shared.Attributes;
using Player.Data;

namespace SavegameSelector.Data
{
    [Data]
    public partial class SavegameSelectorData
    {
        [Data] List<PlayerData> _players;
    }
}