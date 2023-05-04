using CityPop.CharacterToTexture.Data;
using CityPop.CharacterToTexture.Views;
using Player.Data;
using TMPro;
using UnityEngine;
using Zen.Core.View;
using Zen.CodeGeneration.DataBinding.Attributes;

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

        void PlayerData.IAddedListener.OnAdded(PlayerData playerData)
        {
            _characterVisuals.CharacterData = playerData.Character;
            _name.text = playerData.Character.Name;
        }

        void PlayerData.IRemovedListener.OnRemoved()
        {
            _characterVisuals.CharacterData = null;
        }
    }
}