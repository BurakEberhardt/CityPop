using UnityEngine;

namespace CityPop.Audio
{
    public sealed class AudioService
    {
        // AudioView _view;

        public AudioService()
        {
            // var operationHandle = Addressables.LoadAssetAsync<GameObject>(nameof(AudioService));
            // var prefab = operationHandle.WaitForCompletion();
            // var gameObject = Object.Instantiate(prefab);
            // gameObject.DontDestroyOnLoad();
            // _view = gameObject.GetComponent<AudioView>();
            // operationHandle.Release();
        }

        public void PlayOneShot(AudioClip audioClip)
        {
            AudioSource.PlayClipAtPoint(audioClip, Vector3.zero);
        }
    }
}