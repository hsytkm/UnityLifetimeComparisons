using System;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    /// <summary>
    /// TypeLifetime.PerContainerTransient is the same as ContainerControlledTransientManager.
    /// </summary>
    class PerContainerTransientTypeLifetime : TypeLifetimeBase
    {
        protected override ITypeLifetimeManager GetTypeLifetimeManager() => TypeLifetime.PerContainerTransient;
        public override string? CheckupIndividual() { return null; }

    }
}
