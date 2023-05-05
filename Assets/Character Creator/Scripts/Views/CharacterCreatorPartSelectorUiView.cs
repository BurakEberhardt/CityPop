using CharacterCreator.Data;
using Zen.Core.Extensions;
using UnityEngine;
using UnityEngine.UI;
using Zen.Core.View;
using Zen.CodeGeneration.DataBinding.Attributes;

namespace CityPop.CharacterCreator.Views
{
    [DataBinding(typeof(CharacterCreatorPartSelectorData))]
    public partial class CharacterCreatorPartSelectorUiView : View
        , CharacterCreatorPartSelectorData.IAddedListener
        , CharacterCreatorPartSelectorData.IRemovedListener
    {
        [SerializeField] Button _prevButton;
        [SerializeField] Button _nextButton;

        void Previous() => CharacterCreatorPartSelectorData.Index = (CharacterCreatorPartSelectorData.Index - 1).Mod(CharacterCreatorPartSelectorData.Count);
        void Next() => CharacterCreatorPartSelectorData.Index = (CharacterCreatorPartSelectorData.Index + 1).Mod(CharacterCreatorPartSelectorData.Count);

        void CharacterCreatorPartSelectorData.IAddedListener.OnAdded(CharacterCreatorPartSelectorData characterCreatorPartSelectorData)
        {
            _prevButton.onClick.AddListener(Previous);
            _nextButton.onClick.AddListener(Next);
        }

        void CharacterCreatorPartSelectorData.IRemovedListener.OnRemoved()
        {
            _prevButton.onClick.RemoveListener(Previous);
            _nextButton.onClick.RemoveListener(Next);
        }
    }
}