using System;
using CityPop.CharacterToTexture.Data;
using CityPop.CharacterToTexture.Views;
using Player.Data;
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
        [SerializeField] Button _button;

        public event Action<PlayerData> EventClicked;
        
        void PlayerData.IAddedListener.OnAdded(PlayerData playerData)
        {
            _characterVisuals.CharacterData = playerData.Character;
            _name.text = playerData.Character.Name;
            _button.onClick.AddListener(OnClick);
        }

        void PlayerData.IRemovedListener.OnRemoved()
        {
            _characterVisuals.CharacterData = null;
            _button.onClick.RemoveListener(OnClick);
        }

        void OnClick()
        {
            EventClicked?.Invoke(PlayerData);
        }
    }
}