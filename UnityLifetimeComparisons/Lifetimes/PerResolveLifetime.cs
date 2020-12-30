using System;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    class PerResolveLifetime : LifetimeBase
    {
        public override string TypeLifeTimeName => nameof(TypeLifetime.PerResolve);
        protected override ITypeLifetimeManager GetTypeLifetimeManager() => TypeLifetime.PerResolve;

    }
}
