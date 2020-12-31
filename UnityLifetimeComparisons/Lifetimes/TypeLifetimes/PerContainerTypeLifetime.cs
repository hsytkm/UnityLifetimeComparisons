using System;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    /// <summary>
    /// TypeLifetime.PerContainer is the same as ContainerControlledLifetimeManager.
    /// </summary>
    class PerContainerTypeLifetime : TypeLifetimeBase
    {
        // TypeLifetime.PerContainer and TypeLifetime.ContainerControlled are same.
        protected override ITypeLifetimeManager GetTypeLifetimeManager() => TypeLifetime.PerContainer;
        public override string? CheckupIndividual() { return null; }

    }
}
