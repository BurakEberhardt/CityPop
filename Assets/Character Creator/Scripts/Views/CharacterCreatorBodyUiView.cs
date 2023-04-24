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

        BodyVisualsData _bodyVisualsData;
        public BodyVisualsData BodyVisualsData
        {
            get => _bodyVisualsData;
            set
            {
                if (_bodyVisualsData != null)
                {
                    _bodyVisualsData.RemoveTypeListener(this);
                    _bodyVisualsData.RemoveColorListener(this);
                }

                _bodyVisualsData = value;

                if (_bodyVisualsData != null)
                {
                    _bodyVisualsData.AddTypeListener(this);
                    _bodyVisualsData.AddColorListener(this);
                }
            }
        }
        
        CharacterCreatorBodyConfiguration _characterCreatorBodyConfiguration;
        public CharacterCreatorBodyConfiguration CharacterCreatorBodyConfiguration
        {
            get => _characterCreatorBodyConfiguration;
            set
            {
                if (_characterCreatorBodyConfiguration != null)
                {
                    OnCharacterCreatorBodyConfigurationRemoved();
                }

                _characterCreatorBodyConfiguration = value;

                if (_characterCreatorBodyConfiguration != null)
                {
                    OnCharacterCreatorBodyConfiguration(_characterCreatorBodyConfiguration);
                }
            }
        }

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
      
        public async void OnBodyType(BodyType type)
        {
            var bodyAsset = CharacterVisualsAddressables.GetBodyVisualsConfiguration(type);
            var configuration = await bodyAsset.Task;

            _iconImage.sprite = configuration.Sprite;
        }


        public void OnBodyColor(Color32 color)
        {
            _iconImage.color = color;
        }

        public void OnCharacterCreatorBodyConfiguration(CharacterCreatorBodyConfiguration data)
        {
            _prevBodyButton.onClick.AddListener(PrevBody);
            _nextBodyButton.onClick.AddListener(NextBody);
            _bodyIndex = 0;
            UpdateBody();
        }

        public void OnCharacterCreatorBodyConfigurationRemoved()
        {
            _prevBodyButton.onClick.RemoveListener(PrevBody);
            _nextBodyButton.onClick.RemoveListener(NextBody);
        }
    }
}