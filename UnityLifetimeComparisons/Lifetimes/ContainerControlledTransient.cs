using System;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    class ContainerControlledTransient : LifetimeBase<ContainerControlledTransientManager>
    {

        public ContainerControlledTransient() : base(TypeLifetime.ContainerControlled)
        {
        }

    }
}
