using UnityEngine;
using UnityEngine.UI;

namespace Zen.Ui.Selectable
{
    [RequireComponent(typeof(Graphic))]
    public class ColorTweenGraphic : SelectableState
    {
        [SerializeField] ColorTweenConfig _config;
        Graphic _graphic;

        public void Awake() => _graphic = GetComponent<Graphic>();

        public override void DoStateTransition(SelectionState state, bool instant)
        {
            if (!_graphic)
                return;

            if (_config == null)
                return;

            var color = state switch
            {
                SelectionState.Normal => _config.Normal,
                SelectionState.Highlighted => _config.Highlighted,
                SelectionState.Pressed => _config.Pressed,
                SelectionState.Selected => _config.Selected,
                SelectionState.Disabled => _config.Disabled,
                _ => _graphic.color
            };

            _graphic.CrossFadeColor(color, instant ? 0f : _config.TransitionDuration, true, true);
        }
    }
}