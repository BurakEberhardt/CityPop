using UnityEngine;
using Zen.Ui.Selectable;

namespace Zen.Ui
{
    public class Button : UnityEngine.UI.Button
    {
        [SerializeField] SelectableState[] _selectableStates;
        
        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            foreach (var selectableState in _selectableStates)
            {
                selectableState.DoStateTransition((SelectableState.SelectionState)state, instant);
            }
        }
    }
}