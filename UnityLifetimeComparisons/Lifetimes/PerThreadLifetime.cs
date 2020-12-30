using System;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    class PerThreadLifetime : LifetimeBase<PerThreadLifetimeManager>
    {

        public PerThreadLifetime() : base(TypeLifetime.ContainerControlled)
        {
        }

    }
}
