using System;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    class PerResolveLifetime : LifetimeBase<PerResolveLifetimeManager>
    {

        public PerResolveLifetime() : base(TypeLifetime.ContainerControlled)
        {
        }

    }
}
