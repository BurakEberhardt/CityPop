using UnityEngine;

namespace Zen.Ui.Selectable
{
    [RequireComponent(typeof(Animator))]
    public class Animation : SelectableState
    {
        [SerializeField] AnimationConfig _config;
        Animator _animator;

        public void Awake() => _animator = GetComponent<Animator>();

        public override void DoStateTransition(SelectionState state, bool instant)
        {
            if (!_animator)
                return;
            
            if (_config == null)
                return;

            var animation = state switch
            {
                SelectionState.Normal => _config.Normal,
                SelectionState.Highlighted => _config.Highlighted,
                SelectionState.Pressed => _config.Pressed,
                SelectionState.Selected => _config.Selected,
                SelectionState.Disabled => _config.Disabled,
                _ => string.Empty
            };
            
            _animator.CrossFade(animation, _config.TransitionDuration);
        }
    }
}