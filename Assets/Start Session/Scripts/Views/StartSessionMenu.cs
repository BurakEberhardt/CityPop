using System;
using CityPop.Character;
using CityPop.Character.Interfaces;
using CityPop.Player.Data;
using TMPro;
using UnityEngine;
using Zen.CodeGeneration.DataBinding.Attributes;
using Zen.CodeGeneration.SerializableInterfaces.Attributes;
using Zen.Core.View;
using Zen.Ui;

namespace StartSession.Views
{
    [DataBinding(typeof(PlayerData))]
    public partial class StartSessionMenu : View
        , PlayerData.ICharacterListener
        , PlayerData.IRemovedListener
    {
        [SerializeField] Button _hostButton;
        [SerializeField] Button _joinButton;
        [SerializeField] TMP_InputField _addressField;
        [SerializeField] Button _changeCharacterButton;
        [SerializeInterface] ICharacterView Character { get; set; }

        public event Action<PlayerData> EventHost;
        public event Action<PlayerData, string> EventJoin;
        public event Action<PlayerData> EventChangeCharacter;

        void PlayerData.IRemovedListener.OnRemoved()
        {
            Character.CharacterData = null;
            
            _hostButton.onClick.RemoveListener(OnHost);
            _joinButton.onClick.RemoveListener(OnJoin);
            _changeCharacterButton.onClick.RemoveListener(OnChangeCharacter);
        }

        [UpdateOnInitialize]
        void PlayerData.ICharacterListener.OnCharacter(CharacterData character)
        {
            Character.CharacterData = character;

            _hostButton.onClick.AddListener(OnHost);
            _joinButton.onClick.AddListener(OnJoin);
            _changeCharacterButton.onClick.AddListener(OnChangeCharacter);
        }

        void OnHost()
        {
            EventHost?.Invoke(PlayerData);
        }

        void OnJoin()
        {
            EventJoin?.Invoke(PlayerData, _addressField.text);
        }

        void OnChangeCharacter()
        {
            EventChangeCharacter?.Invoke(PlayerData);
        }
    }
}