using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using Zen.CodeGeneration.DataBinding.Attributes;
using Zen.CodeGeneration.UnityMethods.Attributes;
using Zen.Core.View;

namespace Zen.Ui
{
    [RequireComponent(typeof(GraphicRaycaster))]
    [RequireComponent(typeof(UiScreenAnimation))]
    [DataBinding(typeof(UiScreenData))]
    public partial class UiScreen : View
        , UiScreenData.IActiveListener
    {
        [SerializeField] PlayableDirector _playableDirector;
        GraphicRaycaster _graphicRaycaster;

        [Awake]
        void Initialize()
        {
            _graphicRaycaster = GetComponent<GraphicRaycaster>();
            _graphicRaycaster = GetComponent<GraphicRaycaster>();
        }

        // public Task Show()
        // {
        //     _playableDirector.Play();
        // }
        
        void UiScreenData.IActiveListener.OnActive(bool active)
        {
            
        }
    }
}