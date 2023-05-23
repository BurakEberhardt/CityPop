using System;
using UnityEngine;
using UnityEngine.UI;
using Zen.CodeGeneration.UnityMethods.Attributes;
using Zen.Core.View;

namespace CityPop.Gameplay.Views
{
    public partial class GameplayUi : View
    {
        [SerializeField] Button _closeButton;

        public event Action EventOnClose;

        [OnEnable]
        void RegisterButtonEvents()
        {
            _closeButton.onClick.AddListener(OnClose);
        }

        [OnDisable]
        void UnregisterButtonEvents()
        {
            _closeButton.onClick.RemoveListener(OnClose);
        }

        void OnClose()
        {
            EventOnClose?.Invoke();
        }
    }
}