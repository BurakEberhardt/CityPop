using Character.Scripts.Data;
using CityPop.Character.Interfaces;
using Zen.CodeGeneration.DataBinding.Attributes;
using Zen.CodeGeneration.SerializableInterfaces.Attributes;
using Zen.Core.View;

namespace CityPop.Character
{
    [DataBinding(typeof(CharacterData))]
    [DataBinding(typeof(CharacterControllerData))]
    public partial class CharacterControllerView : View
        , CharacterData.IAddedListener
        , CharacterData.IRemovedListener
        , CharacterControllerData.IAddedListener
    {
        [SerializeInterface] ICharacterView Character { get; set; }

        void CharacterData.IAddedListener.OnAdded(CharacterData characterData)
        {
            Character.CharacterData = characterData;
        }

        void CharacterData.IRemovedListener.OnRemoved()
        {
            Character.CharacterData = null;
        }

        void CharacterControllerData.IAddedListener.OnAdded(CharacterControllerData characterControllerData)
        {
            transform.localPosition = characterControllerData.StartPosition;
        }
    }
}