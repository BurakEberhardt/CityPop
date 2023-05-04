using UnityEngine;
using Zen.CodeGeneration.DataBinding.Attributes;

namespace Ui.ColorPicker.Predefined.Data
{
    [Data]
    public partial class PredefinedColorPickerColorItemData
    {
        public int Index { get; set; }
        [Data] bool _selected;
        public Color32 Color { get; set; }
    }
}