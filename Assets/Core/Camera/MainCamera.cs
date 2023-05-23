using UnityEngine;
using Zen.Core.View;

namespace CityPop.Core.Camera
{
    public class MainCamera : View
    {
        [SerializeField] UnityEngine.Camera _camera;
        public UnityEngine.Camera Camera => _camera;
    }
}