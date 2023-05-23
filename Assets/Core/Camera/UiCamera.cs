using UnityEngine;
using Zen.Core.View;

namespace CityPop.Core.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class UiCamera : View
    {
        [SerializeField] UnityEngine.Camera _camera;
        public UnityEngine.Camera Camera => _camera;
    }
}