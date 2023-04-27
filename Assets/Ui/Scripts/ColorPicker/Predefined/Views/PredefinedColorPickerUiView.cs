using System.Collections.Generic;
using CityPop.Core;
using CityPop.Core.Shared.Attributes;
using Ui.ColorPicker.Predefined.Data;
using UnityEngine;

namespace Ui.ColorPicker.Predefined.Views
{
    [DataBinding(typeof(PredefinedColorPickerData))]
    public partial class PredefinedColorPickerUiView : View
        , PredefinedColorPickerData.IRemovedListener
        , PredefinedColorPickerData.IAddedListener
        , PredefinedColorPickerData.IIndexListener
    {
        [SerializeField] PredefinedColorPickerColorItemUiView _predefinedColorPickerColorItemUiView;
        [SerializeField] Transform _colorItemParent;
        readonly List<PredefinedColorPickerColorItemUiView> _colorItemViews = new();
        int? _selectedIndex;
        
        void PredefinedColorPickerData.IRemovedListener.OnRemoved()
        {
            foreach (var colorItemView in _colorItemViews)
                Destroy(colorItemView.gameObject);

            _selectedIndex = null;
        }

        void PredefinedColorPickerData.IAddedListener.OnAdded(PredefinedColorPickerData predefinedColorPickerData)
        {
            for (var i = 0; i < predefinedColorPickerData.Colors.Length; i++)
            {
                var color = predefinedColorPickerData.Colors[i];
                var colorItemUiView = Instantiate(_predefinedColorPickerColorItemUiView, _colorItemParent);
                
                colorItemUiView.PredefinedColorPickerData = predefinedColorPickerData;
                colorItemUiView.PredefinedColorPickerColorItemData = new PredefinedColorPickerColorItemData()
                {
                    Color = color,
                    Index = i,
                };
                
                _colorItemViews.Add(colorItemUiView);
            }
        }

        [UpdateOnInitialize]
        void PredefinedColorPickerData.IIndexListener.OnIndex(int index)
        {
            if (_selectedIndex.HasValue)
                _colorItemViews[_selectedIndex.Value].PredefinedColorPickerColorItemData.Selected = false;
            
            _colorItemViews[index].PredefinedColorPickerColorItemData.Selected = true;
            _selectedIndex = index;
        }
    }
}