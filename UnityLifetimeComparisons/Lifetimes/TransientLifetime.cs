using System;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    class TransientLifetime : LifetimeBase
    {
        public override string TypeLifeTimeName => nameof(TypeLifetime.Transient);
        protected override ITypeLifetimeManager GetTypeLifetimeManager() => TypeLifetime.Transient;

    }
}
