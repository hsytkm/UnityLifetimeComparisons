using System;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    class HierarchicalLifetime : LifetimeBase
    {
        // Hierarchical と Scoped は同じ
        public override string TypeLifeTimeName => nameof(TypeLifetime.Hierarchical);
        protected override ITypeLifetimeManager GetTypeLifetimeManager() => TypeLifetime.Hierarchical;

    }
}
