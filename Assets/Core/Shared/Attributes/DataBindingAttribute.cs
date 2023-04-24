using System;

namespace CityPop.Core.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
    public class DataBindingAttribute : Attribute
    {
        public readonly Type Type;

        public DataBindingAttribute(Type type)
        {
            Type = type;
        }
    }
}