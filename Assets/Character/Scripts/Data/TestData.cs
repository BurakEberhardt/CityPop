using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CityPop.Core.Shared.Attributes;
using UnityEngine;
using Zen.CodeGeneration;

namespace CityPop.Character
{
    [Data]
    public partial class TestData
    {
        // [SerializeField, Data] int _test;
        // [SerializeField, Data] Color32 _color;
        // [SerializeField, Data] string _name;
        [SerializeField] ListData<int> _players;

        public Zen.CodeGeneration.ListData<int> Players
        {
            get => _players;
            set
            {
                _players = value;
                PlayersListEvents.TargetData = value;
                
                foreach (var listener in _playersListeners)
                    listener.OnPlayers(value);
            }
        }

        public interface IPlayersListener
        {
            void OnPlayers(Zen.CodeGeneration.ListData<int> players);
        }

        System.Collections.Generic.HashSet<IPlayersListener> _playersListeners = new();
        public void AddPlayersListener(IPlayersListener listener) => _playersListeners.Add(listener);
        public void RemovePlayersListener(IPlayersListener listener) => _playersListeners.Remove(listener);

        public readonly PlayersList PlayersListEvents = new();
        public class PlayersList : ListEvents<int> { }
    }
}