using DG.Tweening;
using UnityEngine;
using Zen.Ui.UiEffects;

namespace Zen.Ui.Selectable
{
    [RequireComponent(typeof(UiEmission))]
    public class Emission : SelectableState
    {
        [SerializeField] EmissionConfig _config;
        UiEmission _emission;
        Tweener _tween;

        public void Awake() => _emission = GetComponent<UiEmission>();

        void OnDisable()
        {
            _tween?.Kill(true);
        }

        public override void DoStateTransition(SelectionState state, bool instant)
        {
            if (!_emission)
                return;
            
            if (_config == null)
                return;

            var config = state switch
            {
                SelectionState.Normal => _config.Normal,
                SelectionState.Highlighted => _config.Highlighted,
                SelectionState.Pressed => _config.Pressed,
                SelectionState.Selected => _config.Selected,
                SelectionState.Disabled => _config.Disabled,
                _ => null
            };

            if (config != null)
            {
                var color = _emission.EmissionColor;
                var emissionStrength = _emission.EmissionStrength;

                _tween?.Kill();
                _tween = DOVirtual.Float(0f, 1f, _config.FadeDuration, t =>
                {
                    _emission.EmissionStrength = Mathf.Lerp(emissionStrength, config.Strength, t);
                    _emission.EmissionColor = Color.Lerp(color, config.Color, t);
                });
            }
        }
    }
}