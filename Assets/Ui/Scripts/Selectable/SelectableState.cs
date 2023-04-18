using UnityEngine;

namespace Zen.Ui.Selectable
{
    public abstract class SelectableState : MonoBehaviour
    {
        public enum SelectionState
        {
            Normal,
            Highlighted,
            Pressed,
            Selected,
            Disabled,
        }

        public abstract void DoStateTransition(SelectionState state, bool instant);
    }
}