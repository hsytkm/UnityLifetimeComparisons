using System;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    class ContainerControlledTransient : LifetimeBase
    {
        public override string TypeLifeTimeName => nameof(TypeLifetime.PerContainerTransient);
        protected override ITypeLifetimeManager GetTypeLifetimeManager() => TypeLifetime.PerContainerTransient;

    }
}
