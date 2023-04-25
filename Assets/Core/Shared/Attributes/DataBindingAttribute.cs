using System;

namespace CityPop.Core.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
    public class DataBindingAttribute : Attribute
    {
        public Type Type { get; }
        public Accessibility Accessibility { get; }

        public DataBindingAttribute(Type type, Accessibility accessibility = Accessibility.Private)
        {
            Type = type;
            Accessibility = accessibility;
        }
    }

    public enum Accessibility
    {
        Private,
        Protected,
        Public
    }
}