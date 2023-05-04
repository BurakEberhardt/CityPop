using UnityEngine;
using Zen.CodeGeneration.UnityMethods.Attributes;
using Zen.Core.View;

namespace CityPop.Character
{
    public partial class TestView2 : View
    {
        [Awake]
        void CustomAwake1()
        {
            Debug.Log(nameof(CustomAwake1));
        }
        
        [Awake]
        void CustomAwake2()
        {
            Debug.Log(nameof(CustomAwake2));
        }
        
        [Start]
        void CustomStart()
        {
            Debug.Log(nameof(CustomStart));
        }
        
        [OnEnable]
        void CustomOnEnable()
        {
            Debug.Log(nameof(CustomOnEnable));
        }
        
        [OnDisable]
        void CustomOnDisable()
        {
            Debug.Log(nameof(CustomOnDisable));
        }
        
        [EarlyUpdate]
        void CustomEarlyUpdate()
        {
            Debug.Log(nameof(CustomEarlyUpdate));
        }
        
        [Update]
        void CustomUpdate()
        {
            Debug.Log(nameof(CustomUpdate));
        }
        
        [FixedUpdate]
        void CustomFixedUpdate()
        {
            Debug.Log(nameof(CustomFixedUpdate));
        }
        
        [PreLateUpdate]
        void CustomPreLateUpdate()
        {
            Debug.Log(nameof(CustomPreLateUpdate));
        }
        
        [PostLateUpdate]
        void CustomPostLateUpdate()
        {
            Debug.Log(nameof(CustomPostLateUpdate));
        }
        
        [OnDestroy]
        void CustomOnDestroy()
        {
            Debug.Log(nameof(CustomOnDestroy));
        }
    }
}