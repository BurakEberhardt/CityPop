using CityPop.Character.Interfaces;
using TMPro;
using UnityEngine;
using Zen.CodeGeneration.DataBinding.Attributes;
using Zen.CodeGeneration.SerializableInterfaces.Attributes;
using Zen.Core.View;

namespace CityPop.Character
{

    [DataBinding(typeof(CharacterData))]
    public partial class CharacterView : View, ICharacterView
        , CharacterData.IVisualsListener
        , CharacterData.INameListener
        , CharacterData.IRemovedListener
    {
        [SerializeInterface] ICharacterVisualsView Visuals { get; set; }
        [SerializeField] TextMeshProUGUI _name;

        [UpdateOnInitialize]
        void CharacterData.IVisualsListener.OnVisuals(CharacterVisualsData visuals)
        {
            Visuals.CharacterVisualsData = visuals;
        }

        [UpdateOnInitialize]
        void CharacterData.INameListener.OnName(string name)
        {
            _name?.SetText(name);
        }

        void CharacterData.IRemovedListener.OnRemoved()
        {
            Visuals.CharacterVisualsData = null;
            _name?.SetText(string.Empty);
        }
    }
}