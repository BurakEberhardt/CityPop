using System;

namespace CodeGeneration.Constants
{
    public class NameConstants
    {
        public const string DataAttributeName = "Data";
        public const string DataAttributeFullName = $"CityPop.Core.Shared.Attributes.{DataAttributeName}Attribute";
        
        public const string DataBindingAttributeName = "DataBinding";
        public const string DataBindingAttributeFullName = $"CityPop.Core.Shared.Attributes.{DataBindingAttributeName}Attribute";

        public const string DataAddedListenerName = "IAddedListener";
        public const string DataAddedListenerMethodName = "OnAdded";
        public const string DataRemovedListenerName = "IRemovedListener";
        public const string DataRemovedListenerMethodName = "OnRemoved";
        
        public const string DataAddBindingListenerName = "Add{0}Listener";
        public const string DataRemoveBindingListenerName = "Remove{0}Listener";
        
        public const string DataBindingListenerName = "I{0}Listener";
        public static readonly int DataBindingListenerNameSubstringIndex = DataBindingListenerName.IndexOf("{0}", StringComparison.Ordinal);
        public static readonly int DataBindingListenerNameSubstringLength = DataBindingListenerName.Length - 3;
        public static string GetNameFromDataBindingListener(string listener) =>
            listener.Substring(DataBindingListenerNameSubstringIndex, listener.Length - DataBindingListenerNameSubstringLength);
        public const string DataBindingListenerAddedName = "On{0}Added";
        public const string DataBindingListenerRemovedName = "On{0}Removed";
        
        public const string DataBindingAttributeConstructorTypeType = "System.Type";
        public const string DataBindingAttributeConstructorAccessibilityType = "CityPop.Core.Shared.Attributes.Accessibility";
    }
}