using System;
using Unity;
using Unity.Lifetime;

namespace UnityLifetimeComparisons.Lifetimes
{
    class ExternallyControlledLifetime : LifetimeBase<ExternallyControlledLifetimeManager>
    {

        public ExternallyControlledLifetime() : base(TypeLifetime.ContainerControlled)
        {
        }

    }
}
