using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zen.Core.Extensions;

namespace Core.Context
{
    public static class Context
    {
        public static async Task<T> LoadAsync<T>(string sceneName)
        {
             var asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
             await asyncOperation.GetTask();
             var scene = SceneManager.GetSceneByName(sceneName);
             SceneManager.SetActiveScene(scene);

             foreach (var gameObject in scene.GetRootGameObjects())
             {
                 var result = gameObject.GetComponent<T>();
                 if (result != null)
                     return result;
             }

             return default;
        }
    }
}
