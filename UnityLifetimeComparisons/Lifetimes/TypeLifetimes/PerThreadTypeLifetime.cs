using System;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    /// <summary>
    /// TypeLifetime.PerThread is the same as PerThreadLifetimeManager.
    /// </summary>
    class PerThreadTypeLifetime : TypeLifetimeBase
    {
        protected override ITypeLifetimeManager GetTypeLifetimeManager() => TypeLifetime.PerThread;
        public override string? CheckupIndividual() { return null; }

    }
}
