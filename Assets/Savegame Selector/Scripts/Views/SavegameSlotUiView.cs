using CharacterToTexture.Data;
using CharacterToTexture.Views;
using CityPop.Core;
using CityPop.Core.Shared.Attributes;
using Player.Data;
using TMPro;
using UnityEngine;

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