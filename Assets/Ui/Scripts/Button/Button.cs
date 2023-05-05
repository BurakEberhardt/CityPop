using UnityEngine;
using UnityEngine.EventSystems;
using Zen.Ui.Selectable;

namespace Zen.Ui
{
    public class Button : UnityEngine.UI.Button
    {
        [SerializeField] SelectableState[] _selectableStates;
        SelectionState _state = (SelectionState) (-1);
        bool _pointerDown;

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            if (_state == state)
                return;

            if (_state != SelectionState.Pressed && state == SelectionState.Selected && _pointerDown)
                return;

            _state = state;

            foreach (var selectableState in _selectableStates)
            {
                selectableState.DoStateTransition((SelectableState.SelectionState) state, instant);
            }
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            _pointerDown = true;

            base.OnPointerDown(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            _pointerDown = false;
            base.OnPointerUp(eventData);
        }

        protected override void InstantClearState()
        {
            _pointerDown = false;
            base.InstantClearState();
        }

        protected override void OnEnable()
        {
            _pointerDown = false;
            base.OnEnable();
        }
    }
}