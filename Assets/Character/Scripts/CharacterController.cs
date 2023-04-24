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
            _characterVisuals = Instantiate(Resources.Load<CharacterVisualsView>($"CharacterVisuals-{data.Body.Type}"), transform);
        }

        public void OnCharacterVisualsDataRemoved()
        {
            Destroy(_characterVisuals);
        }
    }
}