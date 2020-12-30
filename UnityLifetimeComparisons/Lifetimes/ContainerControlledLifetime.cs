using System;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    class ContainerControlledLifetime : LifetimeBase
    {
        // ContainerControlled と PerContainer は同じ
        public override string TypeLifeTimeName => nameof(TypeLifetime.PerContainer);
        protected override ITypeLifetimeManager GetTypeLifetimeManager() => TypeLifetime.PerContainer;

    }
}
