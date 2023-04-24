using System;

namespace CityPop.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
    public class ViewDataAttribute : Attribute
    {
        public readonly Type Type;

        public ViewDataAttribute(Type type)
        {
            Type = type;
        }
    }
}