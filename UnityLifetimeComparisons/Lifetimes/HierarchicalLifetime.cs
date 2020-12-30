using System;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    class HierarchicalLifetime : LifetimeBase<HierarchicalLifetimeManager>
    {

        public HierarchicalLifetime() : base(TypeLifetime.ContainerControlled)
        {
        }

    }
}
