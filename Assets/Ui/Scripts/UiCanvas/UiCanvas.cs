using CityPop.Core.Camera;
using Core;
using UnityEngine;
using Zen.Core.View;

namespace Zen.Ui
{
    [RequireComponent(typeof(Canvas))]
    public class UiCanvas: View
    {
        void Start()
        {
            var uiCamera = ServiceLocator.Get<UiCamera>();
            var canvas = GetComponent<Canvas>();
            canvas.worldCamera = uiCamera.Camera;
        }
    }
}