using System;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    class ExternallyControlledLifetime : LifetimeBase
    {
        public override string TypeLifeTimeName => nameof(TypeLifetime.External);
        protected override ITypeLifetimeManager GetTypeLifetimeManager() => TypeLifetime.External;

    }
}
