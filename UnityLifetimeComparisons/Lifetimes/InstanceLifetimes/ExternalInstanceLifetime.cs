using System;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
#if false   // External lifetime is only supported by Instance registrations.
    /// <summary>
    /// InstanceLifetime.External is the same as ExternallyControlledLifetimeManager.
    /// </summary>
    class ExternalInstanceLifetime : InstanceLifetimeBase
    {
        protected override IInstanceLifetimeManager GetInstanceLifetimeManager() => InstanceLifetime.External;
        public override string? CheckupIndividual() { return null; }

    }
#endif
}
