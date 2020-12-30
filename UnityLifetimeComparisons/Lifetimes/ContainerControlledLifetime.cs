using System;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    class ContainerControlledLifetime : LifetimeBase<ContainerControlledLifetimeManager>
    {

        public ContainerControlledLifetime() : base(TypeLifetime.ContainerControlled)
        {
        }

    }
}
