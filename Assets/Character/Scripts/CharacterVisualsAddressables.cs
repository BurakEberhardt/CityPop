using CityPop.Character.Configurations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CityPop.Character
{
    public static class CharacterVisualsAddressables
    {
        public static AsyncOperationHandle<BodyConfiguration> GetBodyVisualsConfiguration(BodyType type)
        {
            return Addressables.LoadAssetAsync<BodyConfiguration>($"Character/Body/{type}");
        }
        
        public static AsyncOperationHandle<HairConfiguration> GetHairVisualsConfiguration(HairType type)
        {
            return Addressables.LoadAssetAsync<HairConfiguration>($"Character/Hair/{type}");
        }
        
        public static AsyncOperationHandle<FaceConfiguration> GetFaceVisualsConfiguration(FaceType type)
        {
            return Addressables.LoadAssetAsync<FaceConfiguration>($"Character/Face/{type}");
        }
    }
}