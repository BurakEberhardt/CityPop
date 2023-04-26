using CityPop.Character;
using CityPop.CharacterCreator.Configurations;
using CityPop.Core;
using CityPop.Core.Shared.Attributes;
using Core.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace CityPop.CharacterCreator.Views
{
    [DataBinding(typeof(HairVisualsData))]
    [DataBinding(typeof(CharacterCreatorHairConfiguration))]
    public partial class CharacterCreatorHairUiView : View
        , HairVisualsData.ITypeListener
        , HairVisualsData.IColorListener
        , CharacterCreatorHairConfiguration.IAddedListener
        , CharacterCreatorHairConfiguration.IRemovedListener
    {
        [SerializeField] Image _iconImage;
        [SerializeField] Button _nextHairButton;
        [SerializeField] Button _prevHairButton;
        int _hairIndex;

        void PrevHair()
        {
            _hairIndex = (--_hairIndex).Mod(_characterCreatorHairConfiguration.Types.Length);
            UpdateHair();
        }

        void NextHair()
        {
            _hairIndex = (++_hairIndex).Mod(_characterCreatorHairConfiguration.Types.Length);
            UpdateHair();
        }

        void UpdateHair()
        {
            _hairVisualsData.Type = _characterCreatorHairConfiguration.Types[_hairIndex];
        }

        void CharacterCreatorHairConfiguration.IAddedListener.OnAdded(CharacterCreatorHairConfiguration characterCreatorHairConfiguration)
        {
            _prevHairButton.onClick.AddListener(PrevHair);
            _nextHairButton.onClick.AddListener(NextHair);
            _hairIndex = 0;
            UpdateHair();
        }

        void CharacterCreatorHairConfiguration.IRemovedListener.OnRemoved()
        {
            _prevHairButton.onClick.RemoveListener(PrevHair);
            _nextHairButton.onClick.RemoveListener(NextHair);
        }

        async void HairVisualsData.ITypeListener.OnType(HairType type)
        {
            var hairAsset = CharacterVisualsAddressables.GetHairVisualsConfiguration(type);
            var configuration = await hairAsset.Task;

            _iconImage.sprite = configuration.Sprite;
        }

        void HairVisualsData.IColorListener.OnColor(Color32 color)
        {
            _iconImage.color = color;
        }
    }
}