using System;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    /// <summary>
    /// TypeLifetime.Hierarchical is the same as HierarchicalLifetimeManager.
    /// </summary>
    class HierarchicalTypeLifetime : TypeLifetimeBase
    {
        // TypeLifetime.Hierarchical and TypeLifetime.Scoped are same.
        protected override ITypeLifetimeManager GetTypeLifetimeManager() => TypeLifetime.Hierarchical;

    }
}
