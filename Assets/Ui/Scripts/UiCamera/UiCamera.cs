using Core;
using UnityEngine;
using Zen.Core.View;

namespace Zen.Ui
{
    [RequireComponent(typeof(Camera))]
    public class UiCamera : View
    {
        public Camera Camera { get; private set; }
        
        void Awake()
        {
            Camera = GetComponent<Camera>();
            ServiceLocator.Add(this);
        }
    }
}