using System;

namespace CityPop.Core.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DeferredUpdateAttribute : Attribute
    {
        public UpdateType UpdateType { get; }

        public DeferredUpdateAttribute(UpdateType updateType = UpdateType.Update)
        {
            UpdateType = updateType;
        }
    }
    
    public enum UpdateType
    {
        EarlyUpdate = 2,
        FixedUpdate = 3,
        PreUpdate = 4,
        Update = 5,
        PreLateUpdate = 6,
        PostLateUpdate = 7
    }
}