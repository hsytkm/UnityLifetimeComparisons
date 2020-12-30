using System;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    class TransientLifetime : LifetimeBase<TransientLifetimeManager>
    {

        public TransientLifetime() : base(TypeLifetime.Transient)
        {
        }

    }
}
