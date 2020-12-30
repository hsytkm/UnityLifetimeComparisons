using System;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    /// <summary>
    /// InstanceLifetime.External is the same as ExternallyControlledLifetimeManager.
    /// </summary>
    class ExternalInstanceLifetime : InstanceLifetimeBase
    {
        protected override IInstanceLifetimeManager GetInstanceLifetimeManager() => InstanceLifetime.External;

    }
}
