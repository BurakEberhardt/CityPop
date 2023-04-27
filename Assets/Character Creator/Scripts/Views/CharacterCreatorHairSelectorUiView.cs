using CharacterCreator.Data;
using CityPop.Character;
using CityPop.CharacterCreator.Configurations;
using CityPop.Core;
using CityPop.Core.Shared.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace CityPop.CharacterCreator.Views
{
    [DataBinding(typeof(HairVisualsData))]
    [DataBinding(typeof(CharacterCreatorHairConfiguration))]
    [DataBinding(typeof(CharacterCreatorPartSelectorData))]
    public partial class CharacterCreatorHairSelectorUiView : View
        , HairVisualsData.ITypeListener
        , HairVisualsData.IColorListener
        , CharacterCreatorHairConfiguration.IAddedListener
        , CharacterCreatorPartSelectorData.IIndexListener
    {
        [SerializeField] CharacterCreatorPartSelectorUiView _partSelectorUi;
        [SerializeField] Image _iconImage;

        void CharacterCreatorHairConfiguration.IAddedListener.OnAdded(CharacterCreatorHairConfiguration characterCreatorHairConfiguration)
        {
            CharacterCreatorPartSelectorData = _partSelectorUi.CharacterCreatorPartSelectorData = new CharacterCreatorPartSelectorData()
            {
                Index = 0,
                Count = characterCreatorHairConfiguration.Types.Length
            };
        }

        [UpdateOnInitialize]
        void CharacterCreatorPartSelectorData.IIndexListener.OnIndex(int index)
        {
            _hairVisualsData.Type = _characterCreatorHairConfiguration.Types[CharacterCreatorPartSelectorData.Index];
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