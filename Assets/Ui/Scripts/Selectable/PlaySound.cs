using CityPop.Audio;
using Core;
using UnityEngine;

namespace Zen.Ui.Selectable
{
    public class PlaySound : SelectableState
    {
        [SerializeField] PlaySoundConfig _config;

        public override void DoStateTransition(SelectionState state, bool instant)
        {
            if (_config == null)
                return;

            var audioClip = state switch
            {
                SelectionState.Normal => _config.Normal,
                SelectionState.Highlighted => _config.Highlighted,
                SelectionState.Pressed => _config.Pressed,
                SelectionState.Selected => _config.Selected,
                SelectionState.Disabled => _config.Disabled,
                _ => null
            };

            if (audioClip != null)
            {
                ServiceLocator.Get<AudioService>().PlayOneShot(audioClip);
            }
        }
    }
}