using System.Collections.Generic;
using Zen.CodeGeneration.DataBinding.Attributes;

namespace Zen.Ui
{
    [Data]
    public sealed partial class UiServiceData
    {
        [Data] Stack<UiScreen> _screens;
    }
}