using CityPop.Core.Camera;
using CityPop.MainMenu.Contexts;
using Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zen.Core.View;

namespace CityPop.Initialization.Contexts
{
    public class InitializationContext : View
    {
        [SerializeField] MainCamera _mainCamera;
        [SerializeField] UiCamera _uiCamera;
        
        async void Start()
        {
            foreach (var go in SceneManager.GetActiveScene().GetRootGameObjects())
                if(go != gameObject)
                    DontDestroyOnLoad(go);
            
            ServiceLocator.Add(_mainCamera);
            ServiceLocator.Add(_uiCamera);
            
            await MainMenuContext.Initialize();
        }
    }
}