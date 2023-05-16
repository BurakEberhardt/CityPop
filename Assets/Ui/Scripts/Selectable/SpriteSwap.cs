using UnityEngine;
using UnityEngine.UI;

namespace Zen.Ui.Selectable
{
    [RequireComponent(typeof(Image))]
    public class SpriteSwap : SelectableState
    {
        [SerializeField] SpriteSwapConfig _config;
        Image _image;

        public void Awake() => _image = GetComponent<Image>();

        public override void DoStateTransition(SelectionState state, bool instant)
        {
            if (!_image)
                return;
            
            if (_config == null)
                return;

            _image.sprite = state switch
            {
                SelectionState.Normal => _config.Normal,
                SelectionState.Highlighted => _config.Highlighted,
                SelectionState.Pressed => _config.Pressed,
                SelectionState.Selected => _config.Selected,
                SelectionState.Disabled => _config.Disabled,
                _ => _image.sprite
            };
        }
    }
}