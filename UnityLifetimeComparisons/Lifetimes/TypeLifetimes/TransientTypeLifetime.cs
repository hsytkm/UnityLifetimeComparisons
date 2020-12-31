using System;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    /// <summary>
    /// TypeLifetime.Transient is the same as TransientLifetimeManager.Instance.
    /// </summary>
    class TransientTypeLifetime : TypeLifetimeBase
    {
        protected override ITypeLifetimeManager GetTypeLifetimeManager() => TypeLifetime.Transient;
        public override string? CheckupIndividual() { return null; }

    }
}
