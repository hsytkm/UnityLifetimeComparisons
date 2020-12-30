using System;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    /// <summary>
    /// InstanceLifetime.PerContainer is the same as ContainerControlledLifetimeManager.
    /// </summary>
    class PerContainerInstanceLifetime : InstanceLifetimeBase
    {
        protected override IInstanceLifetimeManager GetInstanceLifetimeManager() => InstanceLifetime.PerContainer;

    }
}
