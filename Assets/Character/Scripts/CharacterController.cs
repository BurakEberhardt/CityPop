using UnityEngine;

namespace CityPop.Character
{
    public class CharacterController : MonoBehaviour
        , CharacterVisualsData.IAddedListener
        , CharacterVisualsData.IRemovedListener
    {
        CharacterVisualsView _characterVisuals;

        public void OnCharacterVisualsData(CharacterVisualsData data)
        {
        }

        public void OnCharacterVisualsDataRemoved()
        {
        }

        void CharacterVisualsData.IAddedListener.OnAdded(CharacterVisualsData characterVisualsData)
        {
            _characterVisuals = Instantiate(Resources.Load<CharacterVisualsView>($"CharacterVisuals-{characterVisualsData.Body.Type}"), transform);
        }

        void CharacterVisualsData.IRemovedListener.OnRemoved()
        {
            Destroy(_characterVisuals);
        }
    }
}