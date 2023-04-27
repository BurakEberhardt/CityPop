using CityPop.Core.Shared.Attributes;
using UnityEngine;

namespace Ui.ColorPicker.Predefined.Data
{
    [Data]
    public partial class PredefinedColorPickerData
    {
        [Data] int _index;
        public Color32[] Colors { get; set; }
    }
}