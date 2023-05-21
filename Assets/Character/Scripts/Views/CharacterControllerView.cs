using UnityEngine;
using Zen.CodeGeneration.DataBinding.Attributes;
using Zen.Core.View;

namespace CityPop.Character
{
    [DataBinding(typeof(CharacterData))]
    public partial class CharacterControllerView : View
        , CharacterData.IAddedListener
        , CharacterData.IRemovedListener
    {
        [SerializeField] CharacterView _character;

        void CharacterData.IAddedListener.OnAdded(CharacterData characterData)
        {
            _character.CharacterData = characterData;
        }

        void CharacterData.IRemovedListener.OnRemoved()
        {
            _character.CharacterData = null;
        }
    }
}