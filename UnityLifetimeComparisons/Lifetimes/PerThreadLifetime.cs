using System;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    class PerThreadLifetime : LifetimeBase
    {
        public override string TypeLifeTimeName => nameof(TypeLifetime.PerThread);
        protected override ITypeLifetimeManager GetTypeLifetimeManager() => TypeLifetime.PerThread;

    }
}
