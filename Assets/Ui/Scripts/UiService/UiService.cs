using System.Collections.Generic;
using Zen.CodeGeneration.DataBinding.Attributes;

namespace Zen.Ui
{
    [DataBinding(typeof(UiServiceData))]
    public sealed partial class UiService
        : UiServiceData.IScreensListener
    {
        public UiService()
        {
            UiServiceData = new UiServiceData()
            {
                Screens = new Stack<UiScreen>()
            };
        }

        void UiServiceData.IScreensListener.OnScreens(Stack<UiScreen> screens)
        {
            foreach (var screen in screens)
            {
            }
        }
        
        public void PushScreen<T>() where T : UiScreen
        {
            
        }
    }
}