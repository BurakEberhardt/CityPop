using System;
using CityPop.CharacterToTexture.Data;
using CityPop.CharacterToTexture.Views;
using CityPop.Player.Data;
using TMPro;
using UnityEngine;
using Zen.Core.View;
using Zen.CodeGeneration.DataBinding.Attributes;
using Zen.Ui;

namespace SavegameSelector.Views
{
    [DataBinding(typeof(PlayerData))]
    [DataBinding(typeof(CharacterToSpriteData))]
    public partial class SavegameSlotUiView : View
        , PlayerData.IAddedListener
        , PlayerData.IRemovedListener
    {
        [SerializeField] CharacterSpriteView _characterVisuals;
        [SerializeField] TextMeshProUGUI _name;
        [SerializeField] Button _selectButton;
        [SerializeField] Button _editButton;
        [SerializeField] Button _deleteButton;

        public event Action<PlayerData> EventSelected;
        public event Action<PlayerData> EventEdit;
        public event Action<PlayerData> EventDelete;
        
        void PlayerData.IAddedListener.OnAdded(PlayerData playerData)
        {
            _characterVisuals.CharacterData = playerData.Character;
            _name.text = playerData.Character.Name;
            _selectButton.onClick.AddListener(Select);
            _editButton.onClick.AddListener(Edit);
            _deleteButton.onClick.AddListener(Delete);
        }

        void PlayerData.IRemovedListener.OnRemoved()
        {
            _characterVisuals.CharacterData = null;
            _selectButton.onClick.RemoveListener(Select);
            _editButton.onClick.RemoveListener(Edit);
            _deleteButton.onClick.RemoveListener(Delete);
        }

        void Select()
        {
            EventSelected?.Invoke(PlayerData);
        }
        
        void Edit()
        {
            EventEdit?.Invoke(PlayerData);
        }
        
        void Delete()
        {
            EventDelete?.Invoke(PlayerData);
        }
    }
}