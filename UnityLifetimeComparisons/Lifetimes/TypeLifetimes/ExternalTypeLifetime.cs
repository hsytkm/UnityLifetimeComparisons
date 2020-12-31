using System;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
#if false   // External lifetime is only supported by Instance registrations.
    /// <summary>
    /// TypeLifetime.External is the same as ExternallyControlledLifetimeManager.
    /// </summary>
    class ExternalTypeLifetime : TypeLifetimeBase
    {
        protected override ITypeLifetimeManager GetTypeLifetimeManager() => TypeLifetime.External;
        public override string? CheckupIndividual() { return null; }

    }
#endif
}
