using Ui.ColorPicker.Predefined.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zen.Core.View;
using Zen.CodeGeneration.DataBinding.Attributes;

namespace Ui.ColorPicker.Predefined.Views
{
    [DataBinding(typeof(PredefinedColorPickerColorItemData))]
    [DataBinding(typeof(PredefinedColorPickerData))]
    public partial class PredefinedColorPickerColorItemUiView : View
        , PredefinedColorPickerColorItemData.IAddedListener
        , PredefinedColorPickerColorItemData.IRemovedListener
        , PredefinedColorPickerColorItemData.ISelectedListener
        , IPointerClickHandler
    {
        [SerializeField] Color _selectedBorderColor;
        [SerializeField] Image _colorImage;
        [SerializeField] Image _borderImage;

        Color _defaultBorderColor;

        void PredefinedColorPickerColorItemData.IAddedListener.OnAdded(PredefinedColorPickerColorItemData predefinedColorPickerColorItemData)
        {
            _colorImage.color = predefinedColorPickerColorItemData.Color;
        }

        void PredefinedColorPickerColorItemData.IRemovedListener.OnRemoved()
        {
            _colorImage.color = Color.clear;
        }

        [UpdateOnInitialize]
        void PredefinedColorPickerColorItemData.ISelectedListener.OnSelected(bool selected)
        {
            _borderImage.color = selected ? _selectedBorderColor : _defaultBorderColor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            PredefinedColorPickerData.Index = PredefinedColorPickerColorItemData.Index;
        }
    }
}