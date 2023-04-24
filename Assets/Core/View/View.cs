using UnityEngine;

namespace CityPop.Core
{
    public class View : MonoBehaviour
    {
        bool _hasCachedTransform;
        Transform _transform;
        public new Transform transform
        {
            get
            {
                if (!_hasCachedTransform)
                    _transform = base.transform;

                return _transform;
            }
        }
        
        bool _hasCachedGameObject;
        GameObject _gameObject;
        public new GameObject gameObject
        {
            get
            {
                if (!_hasCachedGameObject)
                    _gameObject = base.gameObject;

                return _gameObject;
            }
        }
    }
}