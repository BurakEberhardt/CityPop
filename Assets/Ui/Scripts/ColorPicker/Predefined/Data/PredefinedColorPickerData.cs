using UnityEngine;
using Zen.Shared.Attributes;

namespace Ui.ColorPicker.Predefined.Data
{
    [Data]
    public partial class PredefinedColorPickerData
    {
        [Data] int _index;
        public Color32[] Colors { get; set; }
    }
}