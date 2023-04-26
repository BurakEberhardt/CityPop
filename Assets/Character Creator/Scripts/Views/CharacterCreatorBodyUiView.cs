using CityPop.Character;
using CityPop.CharacterCreator.Configurations;
using CityPop.Core;
using CityPop.Core.Shared.Attributes;
using Core.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace CityPop.CharacterCreator.Views
{
    [DataBinding(typeof(BodyVisualsData))]
    [DataBinding(typeof(CharacterCreatorBodyConfiguration))]
    public partial class CharacterCreatorBodyUiView : View
        , BodyVisualsData.ITypeListener
        , BodyVisualsData.IColorListener
        , CharacterCreatorBodyConfiguration.IAddedListener
        , CharacterCreatorBodyConfiguration.IRemovedListener
    {
        [SerializeField] Image _iconImage;
        [SerializeField] Button _nextBodyButton;
        [SerializeField] Button _prevBodyButton;
        int _bodyIndex;

        void PrevBody()
        {
            _bodyIndex = (--_bodyIndex).Mod(_characterCreatorBodyConfiguration.Types.Length);
            UpdateBody();
        }

        void NextBody()
        {
            _bodyIndex = (++_bodyIndex).Mod(_characterCreatorBodyConfiguration.Types.Length);
            UpdateBody();
        }

        void UpdateBody()
        {
            _bodyVisualsData.Type = _characterCreatorBodyConfiguration.Types[_bodyIndex];
        }

        void CharacterCreatorBodyConfiguration.IAddedListener.OnAdded(CharacterCreatorBodyConfiguration characterCreatorBodyConfiguration)
        {
            _prevBodyButton.onClick.AddListener(PrevBody);
            _nextBodyButton.onClick.AddListener(NextBody);
            _bodyIndex = 0;
            UpdateBody();
        }

        void CharacterCreatorBodyConfiguration.IRemovedListener.OnRemoved()
        {
            _prevBodyButton.onClick.RemoveListener(PrevBody);
            _nextBodyButton.onClick.RemoveListener(NextBody);
        }

        async void BodyVisualsData.ITypeListener.OnType(BodyType type)
        {
            var bodyAsset = CharacterVisualsAddressables.GetBodyVisualsConfiguration(type);
            var configuration = await bodyAsset.Task;

            _iconImage.sprite = configuration.Sprite;
        }

        void BodyVisualsData.IColorListener.OnColor(Color32 color)
        {
            _iconImage.color = color;
        }
    }
}